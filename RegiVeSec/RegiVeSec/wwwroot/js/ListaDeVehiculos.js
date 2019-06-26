var vm;
function initVue() {
    vm = new Vue({
        el: '#app',
        data: {
          vehiculo: [],
          p_Desde: "",
          p_Hasta:""
        },
        methods: {
            obtenerVehiculo: function () {
                $.ajax({
                    //Cambiar a type: POST si necesario
                    type: "GET",


                    // Formato de datos que se espera en la respuesta
                    dataType: "json",
                    // URL a la que se enviará la solicitud Ajax
                    url: "/Vehiculo/Listar",
                })
                    .done(function (data) {
                        vm.$data.vehiculo = data;
                    })
                    .fail(function (jqXHR, textStatus, errorThrown) {
                        if (console && console.log) {
                            console.log("La solicitud ha fallado: " + textStatus);
                        }
                    });

          },
          BuscarVehiculo:function() {


            var filtro = this.p_Desde + "|" + this.p_Hasta;
            $.ajax({
              //Cambiar a type: POST si necesario
              type: "GET",


              // Formato de datos que se espera en la respuesta
              dataType: "json",
              // URL a la que se enviará la solicitud Ajax
              url: "/Vehiculo/Buscar/" + filtro,
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
    })
};
