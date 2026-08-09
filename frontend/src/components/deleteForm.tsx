interface DeleteFormProps {
  isShown: boolean;
  onClose: () => void;
  onConfirm: () => void;
  itemName: string;
}

function DeleteForm({
  isShown,
  onClose,
  onConfirm,
  itemName,
}: DeleteFormProps) {
  if (!isShown) return null;
  return (
    <>
      <div className="modal-backdrop fade show" />

      <div className="modal fade show d-block" role="dialog">
        <div className="modal-dialog modal-dialog-centered">
          <div className="modal-content">
            <div className="modal-header">
              <h1 className="modal-title fs-5">Confirm deleting</h1>
              <button type="button" className="btn-close" onClick={onClose} />
            </div>
            <div className="modal-body">
              <p>Are you sure you want to delete {itemName}?</p>
            </div>
            <div className="modal-footer">
              <button
                type="button"
                className="btn btn-primary"
                onClick={onConfirm}
              >
                Confirm
              </button>
              <button
                type="button"
                className="btn btn-secondary"
                onClick={onClose}
              >
                Cancel
              </button>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}

export default DeleteForm;
