using CrudApplication.Data;
using CrudApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CrudApplication.Controllers
{
    public class EmployeeActionsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EmployeeActionsController(ApplicationDbContext db) 
        {
            _db = db;
        }
        
        
        [HttpGet]

        public async Task<IActionResult> ViewEmployee()
        {
            var employees = await _db.Employees.Include(e => e.Category).OrderBy(e => e.EmployeeDateCreated).ToListAsync();


            var DisplayView = employees.Select(e => new DisplayEmployeeViewModel
            {
                Id = e.Id,
                FullName = e.FullName,
                Email = e.Email,
                Department = e.Department,
                Gender = e.Gender,
                WorkStatus = e.WorkStatus,
                StaffId = e.StaffId,
                PhoneNumber = e.PhoneNumber,
                EmployeeDateCreated = e.EmployeeDateCreated,
                CategoryName = e.Category.CategoryName        
            }).ToList();

            return View(DisplayView);
        }

        [HttpGet]
        public IActionResult CreateEmployee()
        {
            List<Categories> CategoryList = new();

            //Getting data from database using entity framework core
            CategoryList = (from item in _db.NewCategoryTable
                            select item).ToList();

            //Inserting select item into lists
            CategoryList.Insert(0, new Categories { CategoryTableId = 0, CategoryName = "--Please Select Category--"});        

            //Assigning the CategoryList to the ViewBag.ListOfCategory
            ViewBag.ListOfCategory = CategoryList;
            return View();
        }

      
        [HttpPost]

        public async Task<IActionResult> CreateEmployee(AddEmployeeViewModel AddEmployeeData)
        {
            //.. To set data back to the viewbag after posting the form..
            List<Categories> CategoryList = new();

            CategoryList = (from item in _db.NewCategoryTable select item).ToList();
            //Could also be written as CategoryList  = await _db.NewCategoryTable.ToListAsync();

            CategoryList.Insert(0, new Categories { CategoryTableId = 0, CategoryName = "--Please Select Category--" });
            ViewBag.ListOfCategory = CategoryList;


            if (AddEmployeeData == null || AddEmployeeData.CategoryTableId == 0)
            {
                //ModelState.AddModelError("", "Please fill the required fields");
                ViewBag.Error = "Please fill in the required fields or select a valid category!";
            }

            //...Getting the selected value
            //int SelectedValue = AddEmployeeData.CategoryTableId;
            //ViewBag.SelectedValue = SelectedValue;
             
            else 
            {
                var ExistingEmployee = await _db.Employees.FirstOrDefaultAsync(e =>
                    e.Email == AddEmployeeData.Email || e.FullName == AddEmployeeData.FullName || e.PhoneNumber == AddEmployeeData.PhoneNumber);

                if (ExistingEmployee != null)
                {
                    ViewBag.Error = "An employee with same Email, Full Name or Phone Number already exists!" + "" +
                                     "Please enter correct details!";
                }
                else
                {
                    var newCategory = _db.NewCategoryTable.Single(c => c.CategoryTableId == AddEmployeeData.CategoryTableId);

                    var employee = new Employee()
                    {
                        Id = Guid.NewGuid(),
                        FullName = AddEmployeeData.FullName,
                        Email = AddEmployeeData.Email,
                        Department = AddEmployeeData.Department,
                        Gender = AddEmployeeData.Gender,
                        WorkStatus = AddEmployeeData.WorkStatus,
                        StaffId = "EMP" + DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                        PhoneNumber = AddEmployeeData.PhoneNumber,
                        EmployeeDateCreated = AddEmployeeData.EmployeeDateCreated,
                        Category = newCategory
                    };

                    if (employee.Category != null)
                    {
                        await _db.Employees.AddAsync(employee);
                        await _db.SaveChangesAsync();
                        return RedirectToAction("ViewEmployee");
                    }
                    else
                    {
                        ViewBag.Error = "Kindly add a category!";
                        //return View();
                    }
                }
            }
           
            return View();
        }


        [HttpGet]

        public async Task<IActionResult> UpdateEmployee(Guid id)
        {
            var newCategoryList = await _db.NewCategoryTable.ToListAsync();
            newCategoryList.Insert(0, new Categories { CategoryTableId = 0, CategoryName = "--Please Select Category--" });
            ViewBag.ListOfCategory = newCategoryList;

            var employee = await _db.Employees.Include(e => e.Category).SingleOrDefaultAsync(x => x.Id == id);

            if(employee != null) 
            {
                var newViewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    FullName = employee.FullName,
                    Email = employee.Email,
                    Department = employee.Department,
                    Gender = employee.Gender,
                    WorkStatus = employee.WorkStatus,
                    StaffId = employee.StaffId,
                    PhoneNumber = employee.PhoneNumber,
                    EmployeeDateCreated = employee.EmployeeDateCreated,
                    CategoryName = employee.Category.CategoryName,
                    CategoryTableId = employee.Category.CategoryTableId,
                };
                return View(newViewModel);
            }
           
            return RedirectToAction("ViewEmployee");
        }

        [HttpPost]

        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeViewModel UpdateEmployeeData)
        {
            var newCategoryList = await _db.NewCategoryTable.ToListAsync();
            newCategoryList.Insert(0, new Categories { CategoryTableId = 0, CategoryName = "--Please Select Category--" });
            ViewBag.ListOfCategory = newCategoryList;

            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Please fill in the required fields";
            }
            else if (UpdateEmployeeData.CategoryTableId == 0)
            {
                ViewBag.Error = "Please select a valid category!";
            }
            else
            {
                var employeeBio = await _db.Employees.Include(e => e.Category).SingleOrDefaultAsync(x => x.Id == UpdateEmployeeData.Id);

                employeeBio.FullName = UpdateEmployeeData.FullName;
                employeeBio.Email = UpdateEmployeeData.Email;
                employeeBio.Department = UpdateEmployeeData.Department;
                employeeBio.Gender = UpdateEmployeeData.Gender;
                employeeBio.WorkStatus = UpdateEmployeeData.WorkStatus;
                employeeBio.PhoneNumber = UpdateEmployeeData.PhoneNumber;
                employeeBio.Category.CategoryName = UpdateEmployeeData.CategoryName;
                employeeBio.EmployeeDateCreated = UpdateEmployeeData.EmployeeDateCreated;

                //Update the employee's category only if the CategoryName has changed, Updated by the Id.
                if (employeeBio.Category != null && employeeBio.Category.CategoryTableId != UpdateEmployeeData.CategoryTableId)
                {
                    var employeeCategory = await _db.NewCategoryTable.SingleOrDefaultAsync(e => e.CategoryTableId == UpdateEmployeeData.CategoryTableId);

                    if (employeeCategory != null)
                    {
                        employeeBio.Category = employeeCategory;
                    }
                }
                await _db.SaveChangesAsync();
                return RedirectToAction("ViewEmployee");
            }

            return View(UpdateEmployeeData);
        }

        [HttpPost]

        public async Task<IActionResult> RemoveEmployee(UpdateEmployeeViewModel UpdateEmployeeData)
        {
            var employeeBio = await _db.Employees.Include(e => e.Category).SingleOrDefaultAsync(x => x.Id == UpdateEmployeeData.Id);

            if (employeeBio != null)
            {
                _db.Employees.Remove(employeeBio);
                await _db.SaveChangesAsync();

                return RedirectToAction("ViewEmployee");
            }
            return RedirectToAction("ViewEmployee");
        }


        [HttpGet]

        public IActionResult AddEmployeeCategory()
        {
            ViewBag.Message = TempData["Message"];
            ViewBag.Message2 = TempData["Message2"];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeCategory(AddEmployeeCatViewModel NewEmployeeCatModel)
        {
            var ExistingCategory = await _db.NewCategoryTable.FirstOrDefaultAsync(k => k.CategoryName == NewEmployeeCatModel.CategoryName);

            if(ExistingCategory != null)
            {
                //ViewBag.Message2 = "This Category already exists! please add another";
                TempData["Message2"] = "This Category already exists! please add another";
                return RedirectToAction("AddEmployeeCategory");
            } 
            else{
                //Categories employeeCategory = new()
                var employeeCategory = new Categories()
                {
                    CategoryName = NewEmployeeCatModel.CategoryName,
                    DateCreated = DateTime.Now
                };

                if (employeeCategory != null)
                {
                    //ViewBag.Message = "A new category has been added successfully!";
                    TempData["Message"] = "A new category has been added successfully!";

                    await _db.NewCategoryTable.AddAsync(employeeCategory);
                    await _db.SaveChangesAsync();
                }
                //ModelState.Clear();
                //return View();
                return RedirectToAction("AddEmployeeCategory");
            }
            //return View();
        }

    }
}
