using Kernel.Cryptography.CertificateManagement;

namespace Kernel.Federation.MetaData.Configuration.Cryptography
{
    public class FileStoreCertificateConfiguration : CertificateStore<FileSystemStore>
    {
        public FileStoreCertificateConfiguration(FileSystemStore store) : base(store)
        {
        }
    }
}