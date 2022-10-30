namespace OurTube.API.Schemas.Inputs
{
    public class CreateRoomInputType
    {
        public String Name { get; set; }
        public CreateRoomConfigurationInputType Configuration { get; set; }
    }
}
