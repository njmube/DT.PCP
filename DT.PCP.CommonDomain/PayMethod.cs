namespace DT.PCP.CommonDomain
{
    /// <summary>
    /// Способы оплаты
    /// </summary>
    public enum PayMethod
    {
        Acquire = 0, // Интернет эквайринг (то-есть карточкой)
        AcquireByCardReader = 1, // Интернет эквайринг (с ипользованием карт-ридера)
        Account = 2, // Лицевого счета
        Atm = 3, // Терминалы, банкоматы и интернет-банкинг
        PayOrder = 4, // Счет-завки (оплачиваются в отделениях РКО)
        ManualPay = 5, // Оплата вручную
        PayRegister = 6 // Реестр платежей
    }
}
