namespace PlayStore.Models.ViewModels
{
    public class PagingInfo
    {
        //Класс для визуализации номеров страниц

        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }
}
