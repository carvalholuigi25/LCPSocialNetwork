import * as bootstrap from 'bootstrap';

function SetNotifications() {
    var dv = document.createElement('div');
    var dv2 = document.createElement('div');
    var dv3 = document.createElement('div');
    
    dv.className = "blknotifications";
    dv.id = "blknotifications";
    dv.setAttribute("aria-live", "polite");
    dv.setAttribute("aria-atomic", "true");
    
    dv2.className = "toast-container tnotification";
    dv2.id = "tnotification";
    
    dv3.className = "blksubnotifications";
    dv3.id = "blksubnotifications";

    dv2.appendChild(dv3);
    dv.appendChild(dv2);

    if(document.getElementsByTagName('div')[0]) {
        document.body.insertBefore(dv, document.getElementsByTagName('div')[0]);
    } else {
        document.body.appendChild(dv);
    }
}

function DisplayNotifications(id = "blksubnotifications", autoshow = false) {
    if(autoshow && document.getElementById('my'+id)) {
        const toast = new bootstrap.Toast(document.getElementById('my'+id));
        toast.show();
    }
}

function GetNotifications(
    id = "blksubnotifications", clname = "text-bg-success", iconame = "bi-check-lg", 
    content = "hello", autoshow = false, delay = 1000
) {
	if(document.querySelector('#'+id)) {
		document.querySelector('#'+id).innerHTML = `
		<div class="toast align-items-center border-0 ${clname} my${id}" id="my${id}" role="alert" aria-live="assertive" aria-atomic="true" data-bs-autohide="true" data-bs-delay="${delay}">
  			<div class="d-flex">
                <div class="toast-body">
                    <i class="bi ${iconame}"></i>
                    ${content.trim()}
                </div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
  			</div>
		</div>`;

        DisplayNotifications(id, autoshow);
	}
}

export { SetNotifications, GetNotifications, DisplayNotifications }