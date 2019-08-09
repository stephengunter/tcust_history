function CloseCustomModal() {
    $('#close-custom-modal').click();
}

function ShowCustomModal(title, content) {
    SetCustomModalContent(title, content)
    $('#open-custom-modal').click();
}

function SetCustomModalContent(title, content) {
    $('#custom-modal-title').text(title);
    $('#custom-modal-content').html(content);  
}

function OnError() {
    alert('系統發生錯誤, 請稍後再試');
}

function isTrue(val) {
    if (!val) return false;
    if (typeof val == 'number') {
        return val > 0
    } else if (typeof val == 'string') {
        if (val.toLowerCase() == 'true') return true
        if (val == '1') return true
        return false
    } else if (typeof val == 'boolean') {
        return val
    }

    return false
}

function isFalse(val) {
	return !isTrue(val);
}