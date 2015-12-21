namespace BusinessLayerLibrary.DAL.Repositories
{
    /// <summary>
    /// ����������� ������������� ������� ����������� ������ � ����� ������ �� ����� ������� ����� �������
    /// ���������� ����������� � ���� ������� �� ������������ �� �������� ����������� � ���������� ������������ - ��� ����������� IDataManager
    /// </summary>
    public interface IRepository
    {}

    public interface IRepository<T> : IRepository
    {
        T Save(T item);

        void Delete(T item);
       
    }

   
}