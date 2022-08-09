namespace AsteroidsAPI.Models
{
    public class AsteroideParameters : QueryStringParameters
    {
        public int days { get; set; }
        public bool ValidDays => days >= 1 && days <= 7;
    }
}
