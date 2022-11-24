namespace PhoneContactProject.Interfaces
{
    internal interface IMyCollection
    {
        public void Add(IRecord record);
        public bool Remove(string name);
    }
}
