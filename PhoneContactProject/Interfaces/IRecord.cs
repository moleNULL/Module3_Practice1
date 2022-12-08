namespace PhoneContactProject.Interfaces
{
    internal interface IRecord : IComparable<IRecord>
    {
        string Name { get; set; }
        string Number { get; set; }
    }
}
