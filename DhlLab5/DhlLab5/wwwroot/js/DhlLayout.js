(() => {
    const toggle = document.querySelector("[data-dhl-sidebar-toggle]");
    if (!toggle) return;

    toggle.addEventListener("click", () => {
        document.body.classList.toggle(
            window.matchMedia("(max-width: 767.98px)").matches
                ? "dhl-sidebar-open"
                : "dhl-sidebar-collapsed"
        );
    });
})();
