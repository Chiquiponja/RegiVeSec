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
            }
        }
    }
    )
});