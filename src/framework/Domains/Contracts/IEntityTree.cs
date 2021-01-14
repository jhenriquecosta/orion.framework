namespace Orion.Framework.Domains
{

    public interface IEntityTree
    {

        int? Ancestral { get; set; }
        int Level { get; set; }
        int Ordem { get; set; }
        string Path { get; set; }
        string Parents { get; set; }
        void InitPath(string path, int level);
    }
}
