
const _modeloEmpleado = {
    idEmpleado: 0,
    nombreCompleto: "",
    idDepartamento: 0,
    sueldo: 0,
    fechaContrato: ""
}

const _modeloSucursal = {
    Sucursal: "",
    Servidor: "",
    Dba: "",
    Usuario: "",
    Password: ""
}

function MostrarSucursales() {

    fetch("/OwnBranches/listaSucursales")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then(responseJson => {
            if (responseJson.length > 0) {

                $("#tablaSucursales tbody").html("");


                responseJson.forEach((sucursal) => {
                    $("#tablaSucursales tbody").append(
                        $("<tr>").append(
                            $("<td>").text(sucursal.branch_name),
                            $("<td>").text(sucursal.server_name),
                            $("<td>").text(sucursal.dba_name),
                            $("<td>").text(sucursal.usr_name),
                            $("<td>").text(sucursal.usr_password),
                            $("<td>").append(
                                $("<button>").addClass("btn btn-primary btn-sm boton-editar-sucursal").text("Editar").data("dataSucursal", sucursal)                               
                            )
                        )
                    )
                })

            }


        })


}


document.addEventListener("DOMContentLoaded", function () {

    MostrarSucursales();

}, false)


function MostrarModal() {

    $("#txtsucursal").val(_modeloSucursal.Sucursal);
    $("#txtservidor").val(_modeloSucursal.Servidor);
    $("#txtbase").val(_modeloSucursal.Dba);
    $("#txtusuario").val(_modeloSucursal.Usuario);
    $("#txtpassword").val(_modeloSucursal.Password);



    $("#modalSucursal").modal("show");

}

$(document).on("click", ".boton-nuevo-empleado", function () {

    _modeloSucursal.Sucursal = "";
    _modeloSucursal.Servidor = "";
    _modeloSucursal.Dba = "";
    _modeloSucursal.Usuario = "";
    _modeloSucursal.Password = "";


    MostrarModal();

})

$(document).on("click", ".boton-editar-sucursal", function () {

    const _sucursal = $(this).data("dataSucursal");


    _modeloSucursal.Sucursal = _sucursal.branch_name;
    _modeloSucursal.Servidor = _sucursal.server_name;
    _modeloSucursal.Dba = _sucursal.dba_name;
    _modeloSucursal.Usuario = _sucursal.usr_name;
    _modeloSucursal.Password = _sucursal.usr_password;

    MostrarModal();

})

$(document).on("click", ".boton-guardar-cambios-sucursal", function () {

    const modelo = {

        branch_name: $("#txtsucursal").val(),
        server_name: $("#txtservidor").val(),
        dba_name: $("#txtbase").val(),
        usr_name: $("#txtusuario").val(),
        usr_password: $("#txtpassword").val()
    }

  
    fetch("/OwnBranches/guardarSucursales", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalSucursal").modal("hide");
                    swal("Datos Escolares Agregados!", "", "success");
                    MostrarSucursales();
                }
                else
                    Swal.fire("Lo sentimos", "No se puedo crear", "error");
            })
})


$(document).on("click", ".boton-eliminar-empleado", function () {

    const _empleado = $(this).data("dataEmpleado");

    Swal.fire({
        title: "Esta seguro?",
        text: `Eliminar empleado "${_empleado.nombreCompleto}"`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, eliminar",
        cancelButtonText: "No, volver"
    }).then((result) => {

        if (result.isConfirmed) {

            fetch(`/Home/eliminarEmpleado?idEmpleado=${_empleado.idEmpleado}`, {
                method: "DELETE"
            })
                .then(response => {
                    return response.ok ? response.json() : Promise.reject(response)
                })
                .then(responseJson => {

                    if (responseJson.valor) {
                        Swal.fire("Listo!", "Empleado fue elminado", "success");
                        MostrarSucursales();
                    }
                    else
                        Swal.fire("Lo sentimos", "No se puedo eliminar", "error");
                })

        }



    })

})