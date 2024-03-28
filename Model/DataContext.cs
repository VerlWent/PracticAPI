namespace PracticeAPI.Model
{
    public partial class DataContext : Model.ShopDataBaseContext
    {
        public static Model.ShopDataBaseContext _context { get; set; } = new Model.ShopDataBaseContext();
    }
}
