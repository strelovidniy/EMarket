using EMarket.Services;
using Microsoft.AspNetCore.Mvc;

namespace EMarket.Controllers
{
    public class SearchController:Controller
    {
        ISearchService searchService = new SearchService();
 
    }
}