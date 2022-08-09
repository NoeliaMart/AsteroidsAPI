namespace AsteroidsAPI.Models
{
    //public class NasaResult
    //{
    //    public DateResults[] near_earth_objects;
    //}

    public class NasaResult
    {
        public Dictionary<string, DateResults[]> near_earth_objects { get; set; }
    }

    public class DateResults
    {
 
        public string name { get; set; }
        public Estimated_Diameter estimated_diameter { get; set; }
        public bool is_potentially_hazardous_asteroid { get; set; }
        public Close_Approach_Data[] close_approach_data { get; set; }
    }

    public class Estimated_Diameter
    {
        public Kilometers kilometers { get; set; }
    }

    public class Kilometers
    {
        public float estimated_diameter_min { get; set; }
        public float estimated_diameter_max { get; set; }
    }


    public class Close_Approach_Data
    {
        public string close_approach_date { get; set; }
        public Relative_Velocity relative_velocity { get; set; }
        public string orbiting_body { get; set; }
    }

    public class Relative_Velocity
    {
        public string kilometers_per_hour { get; set; }
    }

}
