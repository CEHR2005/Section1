using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Section1;

namespace DepartmentManagementApp;

public class Department
{
    public Department(string name, string managerName)
    {
        Name = name;
        ManagerName = managerName;
        Employees = new ObservableCollection<Employee>();
        SubDepartments = new ObservableCollection<Department>();

        if (!string.IsNullOrWhiteSpace(managerName)) AddEmployee(new Employee(managerName));
    }

    public string Name { get; set; }
    public string ManagerName { get; set; }
    public ObservableCollection<Employee> Employees { get; set; }
    public ObservableCollection<Department> SubDepartments { get; set; }

    public int EmployeeCount
    {
        get { return Employees.Count + SubDepartments.Sum(subDept => subDept.EmployeeCount); }
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
        RemoveEmployee(employee);
        newDepartment.AddEmployee(employee);
    }

    public void MoveSubDepartment(Department subDepartment, Department newDepartment)
    {
        RemoveSubDepartment(subDepartment);
        newDepartment.AddSubDepartment(subDepartment);
    }
}

public class Employee
{
    public Employee(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}

public partial class MainWindow : Window
{
    private Department _company = null!;
    private object _selectedItem = null!;

    private List<object> expandedItems = null!;

    public MainWindow()
    {
        InitializeComponent();
        InitializeCompanyStructure();
    }

    private void InitializeCompanyStructure()
    {
        _company = new Department("Head Office", "Chief Executive Officer");


        var companyStructure = new CompanyStructure();

        var departments = companyStructure.Departments;
        var subDepartments = companyStructure.SubDepartments;
        var level4SubDepartments = companyStructure.Level4SubDepartments;

        foreach (var department in departments.Values) _company.AddSubDepartment(department);


        foreach (var departmentKey in subDepartments.Keys)
        {
            var department = departments[departmentKey];
            foreach (var subDepartment in subDepartments[departmentKey])
            {
                department.AddSubDepartment(subDepartment);
                if (level4SubDepartments.ContainsKey(subDepartment.Name))
                    foreach (var level4SubDepartment in level4SubDepartments[subDepartment.Name])
                        subDepartment.AddSubDepartment(level4SubDepartment);
            }
        }

        TreeViewDepartments.ItemsSource = new ObservableCollection<Department> { _company };
    }

    private void TreeViewDepartments_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        _selectedItem = e.NewValue;
    }

    private void TextBox_GotFocus(object sender, RoutedEventArgs e)
    {
        if ((sender as TextBox) is not { Text: "" } textBox) return;
        textBox.Text = "";
        textBox.Foreground = Brushes.Black;
    }

    private void TextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        if ((sender as TextBox) is not { } textBox || !string.IsNullOrWhiteSpace(textBox.Text)) return;
        textBox.Text = "";
        textBox.Foreground = Brushes.Gray;
    }

    private void AddDepartment_Click(object sender, RoutedEventArgs e)
    {
        var departmentName = DepartmentNameInput.Text;
        var managerName = ManagerNameInput.Text;

        if (string.IsNullOrEmpty(departmentName)) return;
        var newDepartment = new Department(departmentName, managerName);

        if (_selectedItem is Department department)
            department.AddSubDepartment(newDepartment);
        else
            _company.AddSubDepartment(newDepartment);

        UpdateTreeView();
    }


    private void AddEmployee_Click(object sender, RoutedEventArgs e)
    {
        var employeeName = EmployeeNameInput.Text;

        if (_selectedItem is not Department department || string.IsNullOrEmpty(employeeName)) return;
        var newEmployee = new Employee(employeeName);
        department.Employees.Add(newEmployee);

        UpdateTreeView();
    }

    private void SaveExpandedItems(IEnumerable items)
    {
        foreach (var item in items)
        {
            if (TreeViewDepartments.ItemContainerGenerator
                    .ContainerFromItem(item) is not TreeViewItem { IsExpanded: true } treeViewItem) continue;
            expandedItems.Add(item);
            SaveExpandedItems(treeViewItem.Items);
        }
    }

    private void RestoreExpandedItems(IEnumerable items)
    {
        foreach (var item in items)
        {
            if (TreeViewDepartments.ItemContainerGenerator.ContainerFromItem(item) is not TreeViewItem treeViewItem)
                continue;
            if (!expandedItems.Contains(item)) continue;
            treeViewItem.IsExpanded = true;
            RestoreExpandedItems(treeViewItem.Items);
        }
    }

    private void UpdateTreeView()
    {
        expandedItems = new List<object>();
        SaveExpandedItems(TreeViewDepartments.Items);

        TreeViewDepartments.ItemsSource = null;
        TreeViewDepartments.ItemsSource = new ObservableCollection<Department> { _company };

        RestoreExpandedItems(TreeViewDepartments.Items);
    }

    private void DeleteDepartment_Click(object sender, RoutedEventArgs e)
    {
        switch (_selectedItem)
        {
            case Employee employee:
            {
                var parentDepartment = FindParentDepartmentForEmployee(_company, employee);
                if (parentDepartment is Department parent)
                {
                    parent.RemoveEmployee(employee);
                    UpdateTreeView();
                }

                break;
            }
            case Department department when department == _company:
                MessageBox.Show("You cannot delete the root department!", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                break;
            case Department department:
            {
                var parentDepartment = FindParentDepartment(_company, department);
                parentDepartment.RemoveSubDepartment(department);
                department = null;
                UpdateTreeView();
                break;
            }
        }
    }

    private static object FindParentDepartmentForEmployee(Department department, Employee employee)
    {
        foreach (var subDepartment in department.SubDepartments)
        {
            if (subDepartment.Employees.Any(emp => emp == employee)) return subDepartment;
            var result = FindParentDepartmentForEmployee(subDepartment, employee);
            if (result != null) return result;
        }

        return null;
    }

    private static Department FindParentDepartment(Department department, Department selectedDepartment1)
    {
        foreach (var subDepartment in department.SubDepartments)
            if (subDepartment == selectedDepartment1)
            {
                return department;
            }
            else
            {
                var result = FindParentDepartment(subDepartment, selectedDepartment1);
                if (result != null) return result;
            }

        return null;
    }

    private void MoveItem_Click(object sender, RoutedEventArgs e)
    {
        if (_selectedItem == null)
        {
            MessageBox.Show("Chose item to move", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        var transferWindow = new TransferWindow(_company, _selectedItem);
        var result = transferWindow.ShowDialog();
        if (result != true) return;
        PerformTransfer(_selectedItem, transferWindow.SelectedTarget);
        UpdateTreeView();
    }


    private void PerformTransfer(object itemToMove, Department targetDepartment)
    {
        switch (itemToMove)
        {
            case Employee employee:
            {
                if (FindParentDepartmentForEmployee(_company, employee) is Department department)
                    department?.MoveEmployee(employee, targetDepartment);

                break;
            }
            case Department department:
            {
                var currentParentDepartment = FindParentDepartment(_company, department);
                currentParentDepartment?.MoveSubDepartment(department, targetDepartment);
                break;
            }
        }
    }
}