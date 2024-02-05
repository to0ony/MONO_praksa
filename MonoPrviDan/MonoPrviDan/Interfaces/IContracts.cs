namespace MonoPrviDan.Interfaces
{
    public interface IContract
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        double TotalCost { get; }
        int ClientID { get; set; }
        int CarID { get; set; }

        void CalculateTotalCost();
        void DisplayContractDetails();
    }

}
