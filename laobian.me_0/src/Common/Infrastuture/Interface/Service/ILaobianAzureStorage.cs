using System.Threading.Tasks;

namespace Laobian.Infrastuture.Interface.Service
{
    public interface ILaobianAzureStorage
    {
        Task<string> UploadFileAsync(string fileName, byte[] content);
    }
}
