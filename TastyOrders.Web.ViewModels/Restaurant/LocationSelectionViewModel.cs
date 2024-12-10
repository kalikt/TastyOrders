namespace TastyOrders.Web.ViewModels.Restaurant
{
    public class LocationSelectionViewModel
    {
        public List<string> Locations { get; set; } = new List<string>();
        public string SelectedLocation { get; set; } = null!;
    }
}
