using Dapper;
using DemoCleanArchitectureV2.Domain.Entities;
using DemoCleanArchitectureV2.Domain.Interfaces;
using DemoCleanArchitectureV2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DemoCleanArchitectureV2.Infrastructure.Repositories
{
    public class MenuRepository : BaseRepository<Menu, int>, IMenuRepository
    {
        public MenuRepository(AppDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Menu>> GetMenusRawSQL()
        {
            const string sql = @"
                        SELECT 
                            m.menu_id AS Id, 
                            m.menu_name AS Name, 
                            m.menu_slug AS Slug,
                            m.menu_display_order AS DisplayOrder,
                            m.menu_is_active AS IsActive,
                            n.new_id AS Id, 
                            n.new_title AS Title,
                            n.new_slug AS Slug
                        FROM Menus m
                        LEFT JOIN NewsMenus nm ON m.menu_id = nm.nm_menu_id
                        LEFT JOIN News n ON nm.nm_news_id = n.new_id
                        WHERE m.menu_is_active = 1";

            var menuDict = new Dictionary<int, Menu>();
            using var connection = context.Database.GetDbConnection();

            await connection.QueryAsync<Menu, News, Menu>(
                sql,
                (menu, news) =>
                {
                    if (!menuDict.TryGetValue(menu.Id, out var currentMenu))
                    {
                        currentMenu = menu;
                        currentMenu.NewsMenu = new List<NewsMenu>();
                        menuDict.Add(currentMenu.Id, currentMenu);
                    }

                    if (news != null && news.Id > 0)
                    {
                        currentMenu.NewsMenu.Add(new NewsMenu
                        {
                            MenuId = currentMenu.Id,
                            NewsId = news.Id,
                            News = news
                        });
                    }
                    return currentMenu;
                },
                splitOn: "Id"
            );

            return menuDict.Values;
        }
    }
}
