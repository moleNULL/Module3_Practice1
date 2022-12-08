namespace PhoneContactProject.Interfaces
{
    internal interface IMyCollection
    {
        public bool Add(IRecord record);
        public bool Remove(string name);
    }
}
