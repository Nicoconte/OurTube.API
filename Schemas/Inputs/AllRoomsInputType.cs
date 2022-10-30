namespace OurTube.API.Schemas.Inputs
{
    public class AllRoomsInputType
    {
        public String? PrivacyType { get; set; } = "*";
        public String? RoomName { get; set; }
        public String? OrderBy { get; set; } = "asc";
    }
}
