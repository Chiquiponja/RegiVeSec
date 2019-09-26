
$().ready(function () {
    
    $("#vehiculoform").validate({


        rules: {


            Propietario  : {
            required: true,
            minlength: 3

            },
            FechaDeIngreso: {
                required: true,
                minlength: 3

            },
            Dominio:   {
            required: true,
            minlength: 3

        },
            Tipo: {
            required: true,

        },
            Marca: {
            required: true,
            minlength: 3

        },
            Color:  {
            required: true,
            minlength: 3

        },
            Modelo: {
            required: true,
            minlength: 3

            },
            MagistradoInterviniente: {
                required: true,
                minlength: 3

            },
            SumarioRegistrar: {
                required: true,
                minlength: 3

            },
            UbicacionActual: {
                required: true,
                minlength: 3

            },
            Causa: {
            required: true,
            minlength: 3

        },
            Estado: {
            required: true,

        },
            NumeroSumario:  {
            required: true,
            minlength: 3

        },
            Orden :  {
            required: true,

        },
            DependenciaProcedente:  {
            required: true,
            minlength: 3

        },
            Observaciones:  {
            required: true,
            minlength: 3

        },
            Recibe: {
            required: true,
            minlength: 3

            },
            Deposito: {
                required: true,
                minlength: 3

            },
            DependenciaRecibe: {
                required: true,
                minlength: 3

            },
            FechaDeEntrega: {
                required: true,
                minlength: 3
            },
            Entrega:  {
            required: true,
            minlength: 3

            }
        

        },
        messages: {
            FechaDeIngreso: {
                required: "Escribi tu Fecha... ",
                minlength: "Minimo 3 caracter"
            },
            Propietario: {
                required: "Escribi tu Propietario... ",
                minlength: "Minimo 3 caracter"
            },
            Dominio: {
                required: "Escribi tu Dominio... ",
                minlength: "Minimo 3 caracter"

            },
            Tipo: {
                required: "Selecciona el Tipo de Vehiculo... ",

            },
            Marca: {
                required: "Escribi tu Marca... ",
                minlength: "Minimo 3 caracter"

            },
            Deposito: {
                required: "Escribi tu Deposito... ",
                minlength: "Minimo 3 caracter"

            },
            Color: {
                required: "Escribi el Color... ",
                minlength: "Minimo 3 caracter"

            },
            Modelo: {
                required: "Escribi tu Modelo... ",
                minlength: "Minimo 3 caracter"

            },
            Causa: {
                required: "Escribi la Causa... ",
                minlength: "Minimo 3 caracter"

            },
            Estado: {
                required: "Escribi tu Estado... ",
                minlength: "Minimo 3 caracter"

            },
            NumeroSumario: {  
                required: "Escribi tu Numero Sumario... ",
                minlength: "Minimo 3 caracter"

            },
            Orden: {
                required: "Escribi tu Orden... ",
                minlength: "Minimo 3 caracter"

            },
            DependenciaProcedente: {
                required: "Escribi la Dependencia Procedente... ",
                minlength: "Minimo 3 caracter"

            },
            SumarioRegistrar: {
                required: "Escribi el Sumario Registrar... ",
                minlength: "Minimo 3 caracter"

            },
            Observaciones: {
                required: "Escribi tu  Observaciones... ",
                minlength: "Minimo 3 caracter"
            },
            Recibe: {
                required: "Escribi quien Recibe... ",
                minlength: "Minimo 3 caracter"

            },
            Deposito: {
                required: "Escribi la Deposito... ",
                minlength: "Minimo 3 caracter"

            },
            MagistradoInterviniente: {
                required: "Escribi el Magistrado Interviniente... ",
                minlength: "Minimo 3 caracter"

            },
            UbicacionActual: {
                required: "Escribi  tu Ubicacion Actual... ",
                minlength: "Minimo 3 caracter"

            },
            FechaDeEntrega: {
                required: "Escribi  la Fecha De Entrega... ",
                minlength: "Minimo 3 caracter"

            },
            Entrega: {
                required: "Escribi la Entrega... ",
                minlength: "Minimo 3 caracter"
            }



        },

        
        submitHandler: function (form) {
            form.submit();
        }
    });

   
});