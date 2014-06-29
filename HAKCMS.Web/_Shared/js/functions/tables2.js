$(function() {


	//===== Datatables =====//

	oTable = $('#data-table2').dataTable({
		"bJQueryUI": false,
		"bAutoWidth": false,
		"sPaginationType": "full_numbers",
		"sDom": '<"datatable-header"fl>t<"datatable-footer"ip>',
		"oLanguage": {
			"sSearch": "<span>Filtrele:</span> _INPUT_",
			"sLengthMenu": "<span>Sayfa Boyutu:</span> _MENU_",
			"oPaginate": { "sFirst": "First", "sLast": "Last", "sNext": ">", "sPrevious": "<" }
		}
    });



	
});
