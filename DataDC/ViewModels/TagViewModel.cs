namespace DataDC.ViewModels
{
    public class TagViewModel
    {
        public string SearchTerm { get; set; }
        public string Tag { get; set; }
        public string SearchValue { get; set; }
        public string TagId
        {
            get { return string.Format("btnTag{0}", Tag); }
        }

    }
}
