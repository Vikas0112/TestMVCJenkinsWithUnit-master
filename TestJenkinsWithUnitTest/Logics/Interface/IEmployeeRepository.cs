﻿using TestJenkinsWithUnit.Models;

namespace TestJenkinsWithUnit.Logics.Interface
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll();
        Employee GetById(int id);
        void Add(Employee employee);
        void Update(Employee employee);
        void Delete(int id);
    }
}
