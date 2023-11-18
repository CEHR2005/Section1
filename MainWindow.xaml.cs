using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace DepartmentManagementApp
{
    public class Department
    {
        public string Name { get; set; }
        public string ManagerName { get; set; }
        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<Department> SubDepartments { get; set; }

        public Department(string name, string managerName)
        {
            Name = name;
            ManagerName = managerName;
            Employees = new ObservableCollection<Employee>();
            SubDepartments = new ObservableCollection<Department>();

            AddEmployee(new Employee(managerName));
        }

        public void AddEmployee(Employee employee)
        {
            Employees.Add(employee);
        }

        public void RemoveEmployee(Employee employee)
        {
            Employees.Remove(employee);
        }

        public void AddSubDepartment(Department department)
        {
            SubDepartments.Add(department);
        }

        public void RemoveSubDepartment(Department department)
        {
            SubDepartments.Remove(department);
        }

        public void MoveEmployee(Employee employee, Department newDepartment)
        {
            Debug.WriteLine("Moving employee " + employee.Name + " to " + newDepartment.Name);
            RemoveEmployee(employee);
            newDepartment.AddEmployee(employee);
        }

        public void MoveSubDepartment(Department subDepartment, Department newDepartment)
        {
            RemoveSubDepartment(subDepartment);
            newDepartment.AddSubDepartment(subDepartment);
        }

        public int EmployeeCount
        {
            get
            {
                int total = Employees.Count;
                foreach (var subDept in SubDepartments)
                {
                    total += subDept.EmployeeCount;
                }
                return total;
            }
        }

        public IEnumerable<object> Items
        {
            get
            {
                foreach (var subDept in SubDepartments)
                    yield return subDept;
                foreach (var emp in Employees)
                    yield return emp;
            }
        }
        public void PrintStructure(int level = 0)
        {
            Debug.WriteLine(new string(' ', level * 2) + Name + " - " + ManagerName);
            foreach (var employee in Employees)
            {
                Debug.WriteLine(new string(' ', (level + 1) * 2) + employee.Name);
            }
            foreach (var subDept in SubDepartments)
            {
                subDept.PrintStructure(level + 1);
            }
        }


    }

    public class Employee
    {
        public string Name { get; set; }

        public Employee(string name)
        {
            Name = name;
        }
    }
    public partial class MainWindow : Window
    {
        private Department company;
        private object selectedItem;
        public MainWindow()
        {
            InitializeComponent();
            InitializeCompanyStructure();
            company.PrintStructure();
        }

        private void InitializeCompanyStructure()
        {
            // Initializing the root department (company)
            company = new Department("Head Office", "Chief Executive Officer");

            // Level 2: Main Departments
            var itDepartment = new Department("IT Department", "John Smith");
            var hrDepartment = new Department("HR Department", "Jane Doe");
            var financeDepartment = new Department("Finance Department", "Alex Brown");
            var marketingDepartment = new Department("Marketing Department", "Sarah Taylor");
            var salesDepartment = new Department("Sales Department", "David Wilson");
            var rndDepartment = new Department("R&D Department", "Emily Clark");
            var customerServiceDepartment = new Department("Customer Service Department", "Brian Martinez");
            var legalDepartment = new Department("Legal Department", "Olivia Garcia");
            var operationsDepartment = new Department("Operations Department", "Michael Anderson");
            var productionDepartment = new Department("Production Department", "Laura Hernandez");
            var supplyChainDepartment = new Department("Supply Chain Department", "Kevin Lee");
            var qualityAssuranceDepartment = new Department("Quality Assurance Department", "Jessica Lewis");
            var corporateStrategyDepartment = new Department("Corporate Strategy Department", "Sophia Martinez");
            var informationSecurityDepartment = new Department("Information Security Department", "Ethan Johnson");
            var trainingAndDevelopmentDepartment = new Department("Training and Development Department", "Chloe Robinson");

            // Adding these departments to the company
            company.AddSubDepartment(itDepartment);
            company.AddSubDepartment(hrDepartment);
            company.AddSubDepartment(financeDepartment);
            company.AddSubDepartment(marketingDepartment);
            company.AddSubDepartment(salesDepartment);
            company.AddSubDepartment(rndDepartment);
            company.AddSubDepartment(customerServiceDepartment);
            company.AddSubDepartment(legalDepartment);
            company.AddSubDepartment(operationsDepartment);
            company.AddSubDepartment(productionDepartment);
            company.AddSubDepartment(supplyChainDepartment);
            company.AddSubDepartment(qualityAssuranceDepartment);
            company.AddSubDepartment(corporateStrategyDepartment);
            company.AddSubDepartment(informationSecurityDepartment);
            company.AddSubDepartment(trainingAndDevelopmentDepartment);


            // Level 3: Sub Departments
            // IT Department Sub-Departments
            itDepartment.AddSubDepartment(new Department("Software Development", "Alice Johnson"));
            itDepartment.AddSubDepartment(new Department("IT Support", "Michael Davis"));

            // HR Department Sub-Departments
            hrDepartment.AddSubDepartment(new Department("Recruitment", "Natalie Portman"));
            hrDepartment.AddSubDepartment(new Department("Employee Relations", "Chris Evans"));

            // Finance Department Sub-Departments
            financeDepartment.AddSubDepartment(new Department("Accounting", "Leonardo DiCaprio"));
            financeDepartment.AddSubDepartment(new Department("Budgeting", "Kate Winslet"));

            // Marketing Department Sub-Departments
            marketingDepartment.AddSubDepartment(new Department("Market Research", "Tom Cruise"));
            marketingDepartment.AddSubDepartment(new Department("Advertising", "Scarlett Johansson"));

            salesDepartment.AddSubDepartment(new Department("Domestic Sales", "Bruce Wayne"));
            salesDepartment.AddSubDepartment(new Department("International Sales", "Clark Kent"));

            // R&D Department Sub-Departments
            rndDepartment.AddSubDepartment(new Department("Product Development", "Tony Stark"));
            rndDepartment.AddSubDepartment(new Department("Process Innovation", "Peter Parker"));

            // Customer Service Department Sub-Departments
            customerServiceDepartment.AddSubDepartment(new Department("Support", "Diana Prince"));
            customerServiceDepartment.AddSubDepartment(new Department("Customer Engagement", "Steve Rogers"));

            // Legal Department Sub-Departments
            legalDepartment.AddSubDepartment(new Department("Corporate Law", "Matt Murdock"));
            legalDepartment.AddSubDepartment(new Department("Intellectual Property", "Jennifer Walters"));

            // Operations Department Sub-Departments
            operationsDepartment.AddSubDepartment(new Department("Logistics", "Natasha Romanoff"));
            operationsDepartment.AddSubDepartment(new Department("Facilities Management", "Nick Fury"));

            // Production Department Sub-Departments
            productionDepartment.AddSubDepartment(new Department("Manufacturing", "Reed Richards"));
            productionDepartment.AddSubDepartment(new Department("Quality Control", "Sue Storm"));

            // Supply Chain Department Sub-Departments
            supplyChainDepartment.AddSubDepartment(new Department("Procurement", "Hal Jordan"));
            supplyChainDepartment.AddSubDepartment(new Department("Distribution", "Barry Allen"));

            // Quality Assurance Department Sub-Departments
            qualityAssuranceDepartment.AddSubDepartment(new Department("Product Testing", "Bruce Banner"));
            qualityAssuranceDepartment.AddSubDepartment(new Department("System Audits", "Stephen Strange"));

            // Corporate Strategy Department Sub-Departments
            corporateStrategyDepartment.AddSubDepartment(new Department("Market Analysis", "Charles Xavier"));
            corporateStrategyDepartment.AddSubDepartment(new Department("Growth Planning", "Erik Lehnsherr"));

            // Information Security Department Sub-Departments
            informationSecurityDepartment.AddSubDepartment(new Department("Cybersecurity", "Jean Grey"));
            informationSecurityDepartment.AddSubDepartment(new Department("Data Privacy", "Scott Summers"));

            // Training and Development Department Sub-Departments
            trainingAndDevelopmentDepartment.AddSubDepartment(new Department("Employee Training", "Ororo Munroe"));
            trainingAndDevelopmentDepartment.AddSubDepartment(new Department("Leadership Development", "Kurt Wagner"));

            // Display in TreeView
            TreeViewDepartments.ItemsSource = new ObservableCollection<Department> { company };

        }
        private void TreeViewDepartments_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            selectedItem = e.NewValue;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void AddDepartment_Click(object sender, RoutedEventArgs e)
        {
            string departmentName = DepartmentNameInput.Text;
            string managerName = "Manager Name";

            if (!string.IsNullOrEmpty(departmentName))
            {
                var newDepartment = new Department(departmentName, managerName);

                if (selectedItem is Department department)
                {
                    department.AddSubDepartment(newDepartment);
                }
                else
                {
                    company.AddSubDepartment(newDepartment);
                }

                UpdateTreeView();
            }
        }


        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            string employeeName = EmployeeNameInput.Text;

            if (selectedItem is Department department && !string.IsNullOrEmpty(employeeName))
            {
                var newEmployee = new Employee(employeeName);
                department.Employees.Add(newEmployee);

                UpdateTreeView();
            }
        }

        private List<object> expandedItems;

        private void SaveExpandedItems(ItemCollection items)
        {
            foreach (var item in items)
            {
                var treeViewItem = TreeViewDepartments.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                if (treeViewItem != null && treeViewItem.IsExpanded)
                {
                    expandedItems.Add(item);
                    SaveExpandedItems(treeViewItem.Items);
                }
            }
        }

        private void RestoreExpandedItems(ItemCollection items)
        {
            foreach (var item in items)
            {
                var treeViewItem = TreeViewDepartments.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                if (treeViewItem != null)
                {
                    if (expandedItems.Contains(item))
                    {
                        treeViewItem.IsExpanded = true;
                        RestoreExpandedItems(treeViewItem.Items);
                    }
                }
            }
        }

        private void UpdateTreeView()
        {
            expandedItems = new List<object>();
            SaveExpandedItems(TreeViewDepartments.Items);

            TreeViewDepartments.ItemsSource = null;
            TreeViewDepartments.ItemsSource = new ObservableCollection<Department> { company };

            RestoreExpandedItems(TreeViewDepartments.Items);
        }

        private void DeleteDepartment_Click(object sender, RoutedEventArgs e)
        {
                if (selectedItem is Employee employee)
                {
                    var parentDepartment = FindParentDepartmentForEmployee(company, employee);
                    if (parentDepartment is Department parent)
                    {
                        parent.RemoveEmployee(employee);
                        UpdateTreeView();
                    }
                }
                else if (selectedItem is Department department)
                {
                    if (department == company)
                    {
                        MessageBox.Show("You cannot delete the root department!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        var parentDepartment = FindParentDepartment(company, department);
                        parentDepartment.RemoveSubDepartment(department);
                        department = null;
                        UpdateTreeView();
                    }

                }
        }

        private object FindParentDepartmentForEmployee(Department department, Employee employee)
        {
            foreach (var subDepartment in department.SubDepartments)
            {
                foreach (var emp in subDepartment.Employees)
                {
                    if (emp == employee)
                    {
                        return subDepartment;
                    }
                }
                var result = FindParentDepartmentForEmployee(subDepartment, employee);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

        private Department FindParentDepartment(Department department, Department selectedDepartment1)
        {
            foreach (var subDepartment in department.SubDepartments)
            {
                if (subDepartment == selectedDepartment1)
                {
                    return department;
                }
                else
                {
                    var result = FindParentDepartment(subDepartment, selectedDepartment1);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }

        private void MoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (selectedItem == null)
            {
                MessageBox.Show("Выберите элемент для переноса.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TransferWindow transferWindow = new TransferWindow(company, selectedItem);
            var result = transferWindow.ShowDialog();
            Debug.WriteLine("Result: " + result);
            if (result == true && transferWindow.SelectedTarget != null)
            {
                Debug.WriteLine("Selected target: " + transferWindow.SelectedTarget.Name);
                PerformTransfer(selectedItem, transferWindow.SelectedTarget);
                UpdateTreeView();
            }
        }



        private void PerformTransfer(object itemToMove, Department targetDepartment)
        {
            Debug.WriteLine("Performing transfer of " + itemToMove + " to " + targetDepartment.Name);
            if (itemToMove is Employee employee)
            {
                Debug.WriteLine("Moving employee " + employee.Name);
                if (FindParentDepartmentForEmployee(company, employee) is Department department)
                {
                    department?.MoveEmployee(employee, targetDepartment);

                }
            }
            else if (itemToMove is Department department)
            {
                Debug.WriteLine("Moving department " + department.Name);
                var currentParentDepartment = FindParentDepartment(company, department);
                currentParentDepartment?.MoveSubDepartment(department, targetDepartment);
            }
        }

    }
}