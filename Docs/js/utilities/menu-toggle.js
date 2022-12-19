import {Collapse} from "bootstrap";

const menuIcon = document.getElementById("toggler-icon");
const navbar = document.getElementById("navbar");

let open = false;

if (navbar) {
	navbar.addEventListener("show.bs.collapse", toggleNav);
	navbar.addEventListener("hide.bs.collapse", toggleNav);
}

function toggleNav(e) {
	menuIcon.classList.toggle("fa-bars");
	menuIcon.classList.toggle("fa-times");

	if (open) {
		document.body.removeEventListener("click", bodyToggleMenu);
	} else {
		document.body.addEventListener("click", bodyToggleMenu);
	}

	open = !open;
	e.stopPropagation();
}

function bodyToggleMenu(e) {
	if (navbar.contains(e.target)) {
		return;
	}

	const collapsible = Collapse.getInstance(navbar);
	collapsible.toggle();
}