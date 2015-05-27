<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="DT.PCP.AzureService" generation="1" functional="0" release="0" Id="b5da95de-a6e7-4433-88d4-131b7908a2c1" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="DT.PCP.AzureServiceGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="DT.PCP.Web.Portal:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/LB:DT.PCP.Web.Portal:Endpoint1" />
          </inToChannel>
        </inPort>
        <inPort name="DT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" protocol="tcp">
          <inToChannel>
            <lBChannelMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/LB:DT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </inToChannel>
        </inPort>
        <inPort name="DT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.WebDeploy.InputEndpoint" protocol="tcp">
          <inToChannel>
            <lBChannelMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/LB:DT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.WebDeploy.InputEndpoint" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="Certificate|DT.PCP.ViolationNotificationWorker:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" defaultValue="">
          <maps>
            <mapMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/MapCertificate|DT.PCP.ViolationNotificationWorker:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </maps>
        </aCS>
        <aCS name="Certificate|DT.PCP.ViolationNotificationWorker:WCFCA" defaultValue="">
          <maps>
            <mapMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/MapCertificate|DT.PCP.ViolationNotificationWorker:WCFCA" />
          </maps>
        </aCS>
        <aCS name="Certificate|DT.PCP.ViolationNotificationWorker:WcfClient" defaultValue="">
          <maps>
            <mapMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/MapCertificate|DT.PCP.ViolationNotificationWorker:WcfClient" />
          </maps>
        </aCS>
        <aCS name="Certificate|DT.PCP.ViolationNotificationWorker:WcfServer" defaultValue="">
          <maps>
            <mapMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/MapCertificate|DT.PCP.ViolationNotificationWorker:WcfServer" />
          </maps>
        </aCS>
        <aCS name="Certificate|DT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" defaultValue="">
          <maps>
            <mapMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/MapCertificate|DT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </maps>
        </aCS>
        <aCS name="Certificate|DT.PCP.Web.Portal:WCFCA" defaultValue="">
          <maps>
            <mapMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/MapCertificate|DT.PCP.Web.Portal:WCFCA" />
          </maps>
        </aCS>
        <aCS name="Certificate|DT.PCP.Web.Portal:WcfClient" defaultValue="">
          <maps>
            <mapMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/MapCertificate|DT.PCP.Web.Portal:WcfClient" />
          </maps>
        </aCS>
        <aCS name="Certificate|DT.PCP.Web.Portal:WcfServer" defaultValue="">
          <maps>
            <mapMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/MapCertificate|DT.PCP.Web.Portal:WcfServer" />
          </maps>
        </aCS>
        <aCS name="DT.PCP.ViolationNotificationWorker:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/MapDT.PCP.ViolationNotificationWorker:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="DT.PCP.ViolationNotificationWorker:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="">
          <maps>
            <mapMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/MapDT.PCP.ViolationNotificationWorker:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </maps>
        </aCS>
        <aCS name="DT.PCP.ViolationNotificationWorker:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="">
          <maps>
            <mapMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/MapDT.PCP.ViolationNotificationWorker:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </maps>
        </aCS>
        <aCS name="DT.PCP.ViolationNotificationWorker:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="">
          <maps>
            <mapMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/MapDT.PCP.ViolationNotificationWorker:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </maps>
        </aCS>
        <aCS name="DT.PCP.ViolationNotificationWorker:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/MapDT.PCP.ViolationNotificationWorker:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </maps>
        </aCS>
        <aCS name="DT.PCP.ViolationNotificationWorkerInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/MapDT.PCP.ViolationNotificationWorkerInstances" />
          </maps>
        </aCS>
        <aCS name="DT.PCP.Web.Portal:DataConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/MapDT.PCP.Web.Portal:DataConnectionString" />
          </maps>
        </aCS>
        <aCS name="DT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/MapDT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="DT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="">
          <maps>
            <mapMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/MapDT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </maps>
        </aCS>
        <aCS name="DT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="">
          <maps>
            <mapMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/MapDT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </maps>
        </aCS>
        <aCS name="DT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="">
          <maps>
            <mapMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/MapDT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </maps>
        </aCS>
        <aCS name="DT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/MapDT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </maps>
        </aCS>
        <aCS name="DT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/MapDT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" />
          </maps>
        </aCS>
        <aCS name="DT.PCP.Web.Portal:ViolationImagesContainer" defaultValue="">
          <maps>
            <mapMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/MapDT.PCP.Web.Portal:ViolationImagesContainer" />
          </maps>
        </aCS>
        <aCS name="DT.PCP.Web.PortalInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/MapDT.PCP.Web.PortalInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:DT.PCP.Web.Portal:Endpoint1">
          <toPorts>
            <inPortMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.Web.Portal/Endpoint1" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:DT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput">
          <toPorts>
            <inPortMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.Web.Portal/Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:DT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.WebDeploy.InputEndpoint">
          <toPorts>
            <inPortMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.Web.Portal/Microsoft.WindowsAzure.Plugins.WebDeploy.InputEndpoint" />
          </toPorts>
        </lBChannel>
        <sFSwitchChannel name="SW:DT.PCP.ViolationNotificationWorker:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp">
          <toPorts>
            <inPortMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.ViolationNotificationWorker/Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
          </toPorts>
        </sFSwitchChannel>
        <sFSwitchChannel name="SW:DT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp">
          <toPorts>
            <inPortMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.Web.Portal/Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
          </toPorts>
        </sFSwitchChannel>
      </channels>
      <maps>
        <map name="MapCertificate|DT.PCP.ViolationNotificationWorker:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" kind="Identity">
          <certificate>
            <certificateMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.ViolationNotificationWorker/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </certificate>
        </map>
        <map name="MapCertificate|DT.PCP.ViolationNotificationWorker:WCFCA" kind="Identity">
          <certificate>
            <certificateMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.ViolationNotificationWorker/WCFCA" />
          </certificate>
        </map>
        <map name="MapCertificate|DT.PCP.ViolationNotificationWorker:WcfClient" kind="Identity">
          <certificate>
            <certificateMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.ViolationNotificationWorker/WcfClient" />
          </certificate>
        </map>
        <map name="MapCertificate|DT.PCP.ViolationNotificationWorker:WcfServer" kind="Identity">
          <certificate>
            <certificateMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.ViolationNotificationWorker/WcfServer" />
          </certificate>
        </map>
        <map name="MapCertificate|DT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" kind="Identity">
          <certificate>
            <certificateMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.Web.Portal/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </certificate>
        </map>
        <map name="MapCertificate|DT.PCP.Web.Portal:WCFCA" kind="Identity">
          <certificate>
            <certificateMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.Web.Portal/WCFCA" />
          </certificate>
        </map>
        <map name="MapCertificate|DT.PCP.Web.Portal:WcfClient" kind="Identity">
          <certificate>
            <certificateMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.Web.Portal/WcfClient" />
          </certificate>
        </map>
        <map name="MapCertificate|DT.PCP.Web.Portal:WcfServer" kind="Identity">
          <certificate>
            <certificateMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.Web.Portal/WcfServer" />
          </certificate>
        </map>
        <map name="MapDT.PCP.ViolationNotificationWorker:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.ViolationNotificationWorker/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapDT.PCP.ViolationNotificationWorker:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" kind="Identity">
          <setting>
            <aCSMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.ViolationNotificationWorker/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </setting>
        </map>
        <map name="MapDT.PCP.ViolationNotificationWorker:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" kind="Identity">
          <setting>
            <aCSMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.ViolationNotificationWorker/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </setting>
        </map>
        <map name="MapDT.PCP.ViolationNotificationWorker:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" kind="Identity">
          <setting>
            <aCSMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.ViolationNotificationWorker/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </setting>
        </map>
        <map name="MapDT.PCP.ViolationNotificationWorker:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.ViolationNotificationWorker/Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </setting>
        </map>
        <map name="MapDT.PCP.ViolationNotificationWorkerInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.ViolationNotificationWorkerInstances" />
          </setting>
        </map>
        <map name="MapDT.PCP.Web.Portal:DataConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.Web.Portal/DataConnectionString" />
          </setting>
        </map>
        <map name="MapDT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.Web.Portal/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapDT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" kind="Identity">
          <setting>
            <aCSMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.Web.Portal/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </setting>
        </map>
        <map name="MapDT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" kind="Identity">
          <setting>
            <aCSMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.Web.Portal/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </setting>
        </map>
        <map name="MapDT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" kind="Identity">
          <setting>
            <aCSMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.Web.Portal/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </setting>
        </map>
        <map name="MapDT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.Web.Portal/Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </setting>
        </map>
        <map name="MapDT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.Web.Portal/Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" />
          </setting>
        </map>
        <map name="MapDT.PCP.Web.Portal:ViolationImagesContainer" kind="Identity">
          <setting>
            <aCSMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.Web.Portal/ViolationImagesContainer" />
          </setting>
        </map>
        <map name="MapDT.PCP.Web.PortalInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.Web.PortalInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="DT.PCP.ViolationNotificationWorker" generation="1" functional="0" release="0" software="C:\Users\sharo_000\Documents\Visual Studio 2012\Projects\DT.PCP\Sources\DT.PCP\DT.PCP.AzureService\csx\Release\roles\DT.PCP.ViolationNotificationWorker" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="1792" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp" portRanges="3389" />
              <outPort name="DT.PCP.ViolationNotificationWorker:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/SW:DT.PCP.ViolationNotificationWorker:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
                </outToChannel>
              </outPort>
              <outPort name="DT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/SW:DT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
                </outToChannel>
              </outPort>
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;DT.PCP.ViolationNotificationWorker&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;DT.PCP.ViolationNotificationWorker&quot;&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;DT.PCP.Web.Portal&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.WebDeploy.InputEndpoint&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
            <storedcertificates>
              <storedCertificate name="Stored0Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" certificateStore="My" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.ViolationNotificationWorker/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
                </certificate>
              </storedCertificate>
              <storedCertificate name="Stored1WCFCA" certificateStore="CA" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.ViolationNotificationWorker/WCFCA" />
                </certificate>
              </storedCertificate>
              <storedCertificate name="Stored2WcfClient" certificateStore="My" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.ViolationNotificationWorker/WcfClient" />
                </certificate>
              </storedCertificate>
              <storedCertificate name="Stored3WcfServer" certificateStore="TrustedPeople" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.ViolationNotificationWorker/WcfServer" />
                </certificate>
              </storedCertificate>
            </storedcertificates>
            <certificates>
              <certificate name="Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
              <certificate name="WCFCA" />
              <certificate name="WcfClient" />
              <certificate name="WcfServer" />
            </certificates>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.ViolationNotificationWorkerInstances" />
            <sCSPolicyUpdateDomainMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.ViolationNotificationWorkerUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.ViolationNotificationWorkerFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="DT.PCP.Web.Portal" generation="1" functional="0" release="0" software="C:\Users\sharo_000\Documents\Visual Studio 2012\Projects\DT.PCP\Sources\DT.PCP\DT.PCP.AzureService\csx\Release\roles\DT.PCP.Web.Portal" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" protocol="tcp" />
              <inPort name="Microsoft.WindowsAzure.Plugins.WebDeploy.InputEndpoint" protocol="tcp" portRanges="8172" />
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp" portRanges="3389" />
              <outPort name="DT.PCP.ViolationNotificationWorker:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/SW:DT.PCP.ViolationNotificationWorker:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
                </outToChannel>
              </outPort>
              <outPort name="DT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/SW:DT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
                </outToChannel>
              </outPort>
            </componentports>
            <settings>
              <aCS name="DataConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" defaultValue="" />
              <aCS name="ViolationImagesContainer" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;DT.PCP.Web.Portal&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;DT.PCP.ViolationNotificationWorker&quot;&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;DT.PCP.Web.Portal&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.WebDeploy.InputEndpoint&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
            <storedcertificates>
              <storedCertificate name="Stored0Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" certificateStore="My" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.Web.Portal/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
                </certificate>
              </storedCertificate>
              <storedCertificate name="Stored1WCFCA" certificateStore="CA" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.Web.Portal/WCFCA" />
                </certificate>
              </storedCertificate>
              <storedCertificate name="Stored2WcfClient" certificateStore="My" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.Web.Portal/WcfClient" />
                </certificate>
              </storedCertificate>
              <storedCertificate name="Stored3WcfServer" certificateStore="TrustedPeople" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.Web.Portal/WcfServer" />
                </certificate>
              </storedCertificate>
            </storedcertificates>
            <certificates>
              <certificate name="Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
              <certificate name="WCFCA" />
              <certificate name="WcfClient" />
              <certificate name="WcfServer" />
            </certificates>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.Web.PortalInstances" />
            <sCSPolicyUpdateDomainMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.Web.PortalUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.Web.PortalFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="DT.PCP.Web.PortalUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="DT.PCP.ViolationNotificationWorkerUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="DT.PCP.ViolationNotificationWorkerFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="DT.PCP.Web.PortalFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="DT.PCP.ViolationNotificationWorkerInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="DT.PCP.Web.PortalInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="42e13687-35ac-405b-b318-587af4ba5451" ref="Microsoft.RedDog.Contract\ServiceContract\DT.PCP.AzureServiceContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="61109620-3bc4-4fbe-b3b0-d05422d91cde" ref="Microsoft.RedDog.Contract\Interface\DT.PCP.Web.Portal:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.Web.Portal:Endpoint1" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="7a0f52e0-40a2-410a-b70b-15a6e83b814c" ref="Microsoft.RedDog.Contract\Interface\DT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="8c947d52-6c0d-4ce8-8a52-5e75961997cc" ref="Microsoft.RedDog.Contract\Interface\DT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.WebDeploy.InputEndpoint@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/DT.PCP.AzureService/DT.PCP.AzureServiceGroup/DT.PCP.Web.Portal:Microsoft.WindowsAzure.Plugins.WebDeploy.InputEndpoint" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>