/**
   * insert image
   *
   * @param {jQuery} $editable
   * @param {String} sUrl
   */
this.insertImage = function ($editable, sUrl, filename) {
    async.createImage(sUrl, filename).then(function ($image) {
        recordUndo($editable);

        $image.css({
            display: '',
            width: Math.min($editable.width(), $image.width())
        });
        range.create().insertNode($image[0]);
        triggerOnChange($editable);
    }).fail(function () {
        var callbacks = $editable.data('callbacks');
        if (callbacks.onImageUploadError) {
            callbacks.onImageUploadError();
        }
    });
};


/**
  * insert Images from file array.
  *
  * @param {jQuery} $editable
  * @param {File[]} files
  */
var insertImages = function ($editable, files) {
    editor.restoreRange($editable);
    var callbacks = $editable.data('callbacks');

    // If onImageUpload options setted
    if (callbacks.onImageUpload) {
        callbacks.onImageUpload(files, editor, $editable);
        // else insert Image as dataURL
    } else {
        $.each(files, function (idx, file) {
            var filename = file.name;
            async.readFileAsDataURL(file).then(function (sDataURL) {
                editor.insertImage($editable, sDataURL, filename);
            }).fail(function () {
                if (callbacks.onImageUploadError) {
                    callbacks.onImageUploadError();
                }
            });
        });
    }
};