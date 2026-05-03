document.addEventListener("DOMContentLoaded", function () {
    const modal = document.getElementById("staticBackdrop");
    const deleteButtons = document.querySelectorAll(".delete-button");

    const modalTitle = modal.querySelector("#staticBackdropLabel");
    const modalProductIdInput = modal.querySelector("#productIdInput");

    deleteButtons.forEach(button => {
        button.addEventListener("click", function () {
            const productId = this.dataset.productId;
            const productName = this.dataset.productName;

            modalTitle.textContent = `Delete ${productName} Product?`;
            modalProductIdInput.value = productId;
        });
    });
});