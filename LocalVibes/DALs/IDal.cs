﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace LocalVibes.DALs
{
    public interface IDal<T> where T : class
    {
        List<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        IEnumerable<SelectListItem> GetAllEnum(Func<T, string> textSelector, Func<T, string> valueSelector);

    }
}
