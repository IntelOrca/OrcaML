namespace OrcaML.Graphics.Shading
{
    public interface IDoStatementBuilder
    {
        void Until(Sbool condition);
        void While(Sbool condition);
    }
}
