namespace Kernel.Federation.MetaData.Configuration.Cryptography
{
    class FileStoreCertificateConfiguration : CertificateConfiguration<FileSystemStore>
    {
        string _certName;
        public FileStoreCertificateConfiguration(FileSystemStore store) : base(store)
        {
        }
    }
}