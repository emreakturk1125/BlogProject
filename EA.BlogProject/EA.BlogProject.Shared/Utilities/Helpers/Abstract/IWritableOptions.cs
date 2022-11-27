using System; 
using Microsoft.Extensions.Options;

namespace EA.BlogProject.Shared.Utilities.Helpers.Abstract
{
    public interface IWritableOptions<out T> : IOptionsSnapshot<T> where T : class, new()
    {
        void Update(Action<T> applyChanges); // (x=>x.Header = "Yeni Başlık") x=>{x.Header = "Yeni Başlık";x.Content="Yeni İçerik"}
    }
}
