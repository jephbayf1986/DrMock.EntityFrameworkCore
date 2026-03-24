namespace DrMock.EfCore.Options
{
    /// <summary>
    /// Mock DbContext Options
    /// Settings defining the behavoir of Mock DbContext
    /// </summary>
    public class MockDbContextOptions
    {
        /// <summary>
        /// Minimum Items to exist in DbSet
        /// Applies to any DbSet that is used within the scope of Unit tests, declared by UseEntity or UseAllEntities 
        /// </summary>
        public int? MinItemsInDbSet { get; set; }
    }
}