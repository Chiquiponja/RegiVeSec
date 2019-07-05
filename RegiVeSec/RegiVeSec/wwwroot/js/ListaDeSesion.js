 var vm;
document.addEventListener('DOMContentLoaded', function () {
    vm = new Vue({
        el: '#appLogin',
        data: {
           sesion: {
                Id: 0,
                Nombre: '',
                Contrasenia: '',
               Estado:''
            },

            Autos: []
        },
        methods: {
            crearsesion: function () {

                $.ajax({
                    url: "/Sesion/Add",
                    contentType: "application/json",
                    async: true,
                    type: "POST",
                    data: data,
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.log("FAIL: " + errorThrown);
                    },
                    success: function (data, textStatus, jqXHR) {
                        console.log("SUCCESS!");
                    }
                });
                
            },
            

        }
    
});
    function Validate() {
        $.ajax(
            {
                type: "POST",
                url: '@Url.Action("Validate", "Account")',
                data: {
                    email: $('#Nombre').val(),
                    password: $('#Contrasenia').val()
                },
                error: function (result) {
                    alert("There is a Problem, Try Again!");
                },
                success: function (result) {
                    console.log(result);
                    if (result.status == true) {
                        window.location.href = '@Url.Action("Index", "Home")';
                    }
                    else {
                        alert(result.message);
                    }
                }
            });
}
    obtenerLoginById: function (id) {
    $.ajax({
        //Cambiar a type: POST si necesario
        type: "GET",
        // Formato de datos que se espera en la respuesta
        dataType: "json",
        // URL a la que se enviará la solicitud Ajax
        url: "/Sesion/GetLoginId/" + id,
    })
        .done(function (data) {
            vm.$data.vehiculo = data;
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            if (console && console.log) {
                console.log("La solicitud ha fallado: " + textStatus);
            }
        });
}
}
)
