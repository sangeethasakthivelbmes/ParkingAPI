namespace Parking.WebAPI.CoreHelper.Model
{
    public class ObjectId
    {
        public Int64 Id { get; set; }
    }
    public class BaseModel: ObjectId
    {
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}

