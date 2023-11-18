using System.Collections.Generic;
using DepartmentManagementApp;

namespace Section1;

internal class CompanyStructure
{
    public Department Company { get; set; } = new("Head Office", "Chief Executive Officer");

    public Dictionary<string, Department> Departments { get; set; } = new()
    {
        { "IT", new Department("IT Department", "John Smith") },
        { "HR", new Department("HR Department", "Jane Doe") },
        { "Finance", new Department("Finance Department", "Alex Brown") },
        { "Marketing", new Department("Marketing Department", "Sarah Taylor") },
        { "Sales", new Department("Sales Department", "David Wilson") },
        { "R&D", new Department("R&D Department", "Emily Clark") },
        { "CustomerService", new Department("Customer Service Department", "Brian Martinez") },
        { "Legal", new Department("Legal Department", "Olivia Garcia") },
        { "Operations", new Department("Operations Department", "Michael Anderson") },
        { "Production", new Department("Production Department", "Laura Hernandez") },
        { "SupplyChain", new Department("Supply Chain Department", "Kevin Lee") },
        { "QualityAssurance", new Department("Quality Assurance Department", "Jessica Lewis") },
        { "CorporateStrategy", new Department("Corporate Strategy Department", "Sophia Martinez") },
        { "InformationSecurity", new Department("Information Security Department", "Ethan Johnson") },
        { "TrainingAndDevelopment", new Department("Training and Development Department", "Chloe Robinson") }
    };

    public Dictionary<string, List<Department>> SubDepartments { get; set; } = new()
    {
        {
            "IT", new List<Department>
            {
                new("Software Development", "Alice Johnson"),
                new("IT Support", "Michael Davis")
            }
        },
        {
            "HR", new List<Department>
            {
                new("Recruitment", "Natalie Portman"),
                new("Employee Relations", "Chris Evans")
            }
        },
        {
            "Finance", new List<Department>
            {
                new("Accounting", "Leonardo DiCaprio"),
                new("Budgeting", "Kate Winslet")
            }
        },
        {
            "Marketing", new List<Department>
            {
                new("Market Research", "Tom Cruise"),
                new("Advertising", "Scarlett Johansson")
            }
        },
        {
            "Sales", new List<Department>
            {
                new("Domestic Sales", "Bruce Wayne"),
                new("International Sales", "Clark Kent")
            }
        },
        {
            "R&D", new List<Department>
            {
                new("Product Development", "Tony Stark"),
                new("Process Innovation", "Peter Parker")
            }
        },
        {
            "CustomerService", new List<Department>
            {
                new("Support", "Diana Prince"),
                new("Customer Engagement", "Steve Rogers")
            }
        },
        {
            "Legal", new List<Department>
            {
                new("Corporate Law", "Matt Murdock"),
                new("Intellectual Property", "Jennifer Walters")
            }
        },
        {
            "Operations", new List<Department>
            {
                new("Logistics", "Natasha Romanoff"),
                new("Facilities Management", "Nick Fury")
            }
        },
        {
            "Production", new List<Department>
            {
                new("Manufacturing", "Reed Richards"),
                new("Quality Control", "Sue Storm")
            }
        },
        {
            "SupplyChain", new List<Department>
            {
                new("Procurement", "Hal Jordan"),
                new("Distribution", "Barry Allen")
            }
        },
        {
            "QualityAssurance", new List<Department>
            {
                new("Product Testing", "Bruce Banner"),
                new("System Audits", "Stephen Strange")
            }
        },
        {
            "CorporateStrategy", new List<Department>
            {
                new("Market Analysis", "Charles Xavier"),
                new("Growth Planning", "Erik Lehnsherr")
            }
        },
        {
            "InformationSecurity", new List<Department>
            {
                new("Cybersecurity", "Jean Grey"),
                new("Data Privacy", "Scott Summers")
            }
        },
        {
            "TrainingAndDevelopment", new List<Department>
            {
                new("Employee Training", "Ororo Munroe"),
                new("Leadership Development", "Kurt Wagner")
            }
        }
    };

    public Dictionary<string, List<Department>> Level4SubDepartments { get; set; } = new()
    {
        {
            "Software Development", new List<Department>
            {
                new("Frontend Development", "Alice Johnson Jr."),
                new("Backend Development", "Michael Davis Jr.")
            }
        },
        {
            "IT Support", new List<Department>
            {
                new("Technical Support", "John Doe"),
                new("Help Desk", "Jane Doe")
            }
        },
        {
            "Recruitment", new List<Department>
            {
                new("Technical Recruiting", "James Smith"),
                new("HR Recruiting", "Mary Johnson")
            }
        }
    };
}