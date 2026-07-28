function loadEmployee() {
  $.ajax({
    url: '/Employee/EmployeeTable',
    type: 'GET',
    success: function (data) {
      $('#employeeTable').html(data);
    },
  });
}

function createEmployee() {
  $.ajax({
    url: '/Employee/CreateEmployee',
    type: 'GET',
    success: function (data) {
      $('#createEmployeeModal .modal-content').html(data);
      const modalElement = document.getElementById('createEmployeeModal');
      const modal = bootstrap.Modal.getOrCreateInstance(modalElement);
      modal.show();
    },
    error: function () {
      alert('Error loading form');
    },
  });
}

$(function () {
  $('#btnCreateEmployee').on('click', createEmployee);
});
