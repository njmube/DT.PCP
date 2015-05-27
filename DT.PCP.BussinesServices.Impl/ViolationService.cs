using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net.Mime;
using System.Web.Hosting;
using DT.PCP.CommonDomain;
using DT.PCP.Domain;
using DT.PCP.Logging;
using DT.PCP.ServicesProxies.BddPayRegisterService;
using DT.PCP.ServicesProxies.BddViolationService;
using DT.PCP.Utils.Impl;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;

namespace DT.PCP.BussinesServices.Impl
{
    public class ViolationService : IViolationService
    {
        private readonly ILogger _logger;
        private readonly ITrafficViolationService _trafficViolationService;
        private readonly ITrafficViolationPayRegister _payRegisterService;

        public ViolationService(ILogger _logger, ITrafficViolationService trafficViolationService, ITrafficViolationPayRegister _payRegisterService)
        {
            this._logger = _logger;
            _trafficViolationService = trafficViolationService;
            this._payRegisterService = _payRegisterService;
        }

        #region Implementation of IViolationService

        public TransportOwnerData GetOwnerInfo(string carNumber, string carPassportNumber)
        {
            return _trafficViolationService.GetTransportOwner(carNumber, carPassportNumber);
        }

        public TrafficViolationServiceResult GetViolations(string carNumber, string carPassportNumber)
        {
            TrafficViolationRequestData requestData = new TrafficViolationRequestData
            {
                //NumberSRTS = carPassportNumber,
                TransportNumber = carNumber
            };


            try
            {
                return _trafficViolationService.GetTrafficViolations(requestData);
            }
            catch (Exception e)
            {
                _logger.Error("GetViolations: ", e);
            }

            return null;
        }

        public TrafficViolationData GetViolationsByOrder(string orderNumber, string carNumber)
        {
            return _trafficViolationService.GetTrafficViolation(carNumber, orderNumber);
        }


        public bool RegisterPayInBdd(PayedViolation payedViolation)
        {
            TrafficViolationPayData payData = new TrafficViolationPayData
            {
                code_bank = "HSBKKZKX",
                CorrelationID = Guid.NewGuid().ToString(),
                n_billing_doc = "String",
                n_prescri = payedViolation.OrderNumber,
                name_payer = payedViolation.FullName,
                pay_penalty = payedViolation.Cost,
                rnn_iin_bin_payer = payedViolation.TaxNumber,
                system_id = payedViolation.PayMethod,
                u_reference_doc = payedViolation.Reference
            };

            RegisterRequestBody requestBody = new RegisterRequestBody(payData);
            RegisterRequest request = new RegisterRequest(requestBody);
            var response = _payRegisterService.Register(request);
            return true;
        }

        public bool RegisterPayInBdd(Order order)
        {
            foreach (var orderDetail in order.Details)
            {
                TrafficViolationPayData payData = new TrafficViolationPayData
                {
                    code_bank = "HSBKKZKX",
                    CorrelationID = Guid.NewGuid().ToString(),
                    n_billing_doc = "String",
                    n_prescri = orderDetail.OrderNumber,
                    name_payer = order.User.FullName,
                    pay_penalty = (double)orderDetail.Cost,
                    rnn_iin_bin_payer = order.User.TaxNumber,
                    system_id = PayMethodAlias.ACQUIRE,
                    u_reference_doc = order.ReferenceCode
                };

                RegisterRequestBody requestBody = new RegisterRequestBody(payData);
                RegisterRequest request = new RegisterRequest(requestBody);
                var response = _payRegisterService.Register(request);

            }

            return true;
        }

        public string GetViolationImagePath(string orderNumber)
        {
            if (RoleEnvironment.IsAvailable)
            {
                if (IsExistInStorage(orderNumber))
                    return GetFromStorage(orderNumber);

                Stream stream = _trafficViolationService.GetTrafficViolationImage(orderNumber);
                var data = Helpers.ReadToEnd(stream);

                EnsureContainerExists();
                var pathFile = SaveImageToBlob(orderNumber, orderNumber + ".jpg", data);

                return pathFile;

            }
            else
            {
                var pathToCache = Path.Combine(HostingEnvironment.ApplicationPhysicalPath,
                    ConfigurationManager.AppSettings["ViolationImageCache"]);

                if (IsExistInCache(orderNumber))
                {
                    return Path.Combine(pathToCache, orderNumber + ".jpg");
                }

                Stream stream = _trafficViolationService.GetTrafficViolationImage(orderNumber);

                var pathToFile = Path.Combine(pathToCache, orderNumber + ".jpg");
                using (var fileStream = File.Create(pathToFile))
                {
                    stream.CopyTo(fileStream);
                }

                return pathToFile;
            }
        }

        private string GetFromStorage(string orderNumber)
        {
            var blob = this.GetContainer().GetBlobReference(orderNumber);
            return blob.Uri.ToString();
        }

        #endregion

        private bool IsExistInCache(string orderNumber)
        {
            var imagePath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, ConfigurationManager.AppSettings["ViolationImageCache"], orderNumber + ".jpg");
            //var imagePath = Path.Combine(ConfigurationManager.AppSettings["ViolationImageCache"], orderNumber + ".jpg");
            return File.Exists(imagePath);
        }

        private bool IsExistInStorage(string orderNumber)
        {
            var blob = this.GetContainer().GetBlobReference(orderNumber);
            return Helpers.IsExistsBlob(blob);
        }

        private CloudBlobContainer GetContainer()
        {
            var account = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("DataConnectionString"));
            var client = account.CreateCloudBlobClient();

            return client.GetContainerReference(RoleEnvironment.GetConfigurationSettingValue("ViolationImagesContainer"));
        }

        private void EnsureContainerExists()
        {
            var container = this.GetContainer();
            container.CreateIfNotExist();

            var permissions = container.GetPermissions();
            permissions.PublicAccess = BlobContainerPublicAccessType.Container;
            container.SetPermissions(permissions);
        }

        private string SaveImageToBlob(string name, string fileName, byte[] data)
        {
            var blob = this.GetContainer().GetBlobReference(name);

            blob.Properties.ContentType = MediaTypeNames.Image.Jpeg;

            var metadata = new NameValueCollection();
            metadata["Id"] = Guid.NewGuid().ToString();
            metadata["Filename"] = fileName;
            metadata["ImageName"] = string.IsNullOrEmpty(name) ? "unknown" : name;

            blob.Metadata.Add(metadata);
            blob.UploadByteArray(data);
            return blob.Uri.ToString();
        }
    }
}
