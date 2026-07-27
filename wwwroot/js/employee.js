function loadEmployee() {
  $.ajax({
    url: '/Employee/Index',
    type: 'GET',
    success: function (data) {
      $('#employeeTable').html(data);
    },
  });
}
