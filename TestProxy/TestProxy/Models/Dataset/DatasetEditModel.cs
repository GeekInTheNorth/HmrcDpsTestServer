namespace TestProxy.Models.Dataset
{
    public class DatasetEditModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string PayeReference { get; set; }

        public bool ShowValidationError { get; set; }
    }
}