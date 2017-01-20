namespace HmrcTpvsProxy.DAL.Repositories
{
    public class BaseRepository
    {
        protected DpsContext context;

        public BaseRepository()
        {
            RefreshContext();
        }

        public void RefreshContext()
        {
            context = new DpsContext();
        }
    }
}
