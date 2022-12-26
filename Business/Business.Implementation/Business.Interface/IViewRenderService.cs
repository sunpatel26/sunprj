using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IViewRenderService
    {
        string RenderToStringAsync(string viewName, object model);
    }
}
