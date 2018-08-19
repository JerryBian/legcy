namespace Laobian.Share.Model.Blob
{
    public class AzureBlobFactory
    {
        public static ContainerAttribute PublicContainer => new ContainerAttribute { Name = "public1", IsPublic = true };

        public static ContainerAttribute DataContainer => new ContainerAttribute { Name = "data1", IsPublic = false };
    }
}

