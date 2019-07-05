var vm;
document.addEventListener('DOMContentLoaded', function () {
    vm = new Vue({
        el: '#appLogin',
        data: {

           seccion: {
                Id: 0,
                Nombre: '',
                Contrasenia: '',
               Estado:''
            },

            Autos: []
        },
        methods: {
            crearseccion: function () {

                $.ajax({
                    url: "/Seccion/Add",
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
            }
        }
    }
    )
});