using InvoiceCreator.InvoiceCreatorDbContext;
using InvoiceCreator.Models.MainModels;

namespace InvoiceCreator.MockingGenerator
{
    public class MockingData
    {
        private readonly InvoiceCreatorInMemoryDbContext _inMemoryContext;
        public MockingData(InvoiceCreatorInMemoryDbContext inMemoryContext) 
        {
            _inMemoryContext = inMemoryContext;
        }
        public void FillInMemoryDatabase()
        {
            Supplier supplier = new Supplier();
            supplier.Name = "Bohuš";
            supplier.Address = "Ambroseho 11";
            supplier.Country = "Slovakia";
            supplier.ICO = 78945612;
            supplier.DIC = 789654123;
            supplier.IsDPHPayer = false;

            PaymentData paymentData = new PaymentData();
            paymentData.Bank = "Slovenská sporiteľňa";
            paymentData.IBAN = "SK92 7894 6541 6542 9874 65478";
            paymentData.SWIFT = "CHUJAS";
            paymentData.VariableSymbol = 78965412;
            paymentData.ConstantSymbol = 0308;

            _inMemoryContext.PaymentDatas.Add(paymentData);
            _inMemoryContext.SaveChanges();

            supplier.PaymentData = paymentData;

            _inMemoryContext.Suppliers.Add(supplier);

            Costumer costumer = new Costumer();
            costumer.Name = "Alexandra Krajmerová";
            costumer.Address = "Ambroseho 11";
            costumer.Country = "Slovakia";
            costumer.ICO = 78945612;
            costumer.DIC = 789654124;
            costumer.IsDPHPayer = false;

            _inMemoryContext.Costumers.Add(costumer);

            Service service = new Service();
            service.NameOfService = "Vyčistenie potrubia";
            service.DescriptionOfService = "Description";
            service.DPH = 20;
            service.UnitPriceWithDPH = 1200;
            service.NumberOfUnits = 1;
            service.UnitPriceWithoutDPH = 1000;
            service.TotalPriceWithDPH = service.UnitPriceWithDPH;

            _inMemoryContext.Services.Add(service);
            _inMemoryContext.SaveChanges();
        }
    }
}
