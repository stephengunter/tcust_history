class Helper {
   static getScrollBarWidth() {
      if (document.documentElement.scrollHeight <= document.documentElement.clientHeight) {
			return 0
		}
		let inner = document.createElement('p')
		inner.style.width = '100%'
		inner.style.height = '200px'

		let outer = document.createElement('div')
		outer.style.position = 'absolute'
		outer.style.top = '0px'
		outer.style.left = '0px'
		outer.style.visibility = 'hidden'
		outer.style.width = '200px'
		outer.style.height = '150px'
		outer.style.overflow = 'hidden'
		outer.appendChild(inner)

		document.body.appendChild(outer)
		let w1 = inner.offsetWidth
		outer.style.overflow = 'scroll'
		let w2 = inner.offsetWidth
		if (w1 === w2) w2 = outer.clientWidth

		document.body.removeChild(outer)

		return (w1 - w2)
   }
   static BusEmitError(error, title) {
      console.log(error)
      let msgtitle = title
      if (error.data && error.data.msg) {
          msgtitle = error.data.msg;
      }
      if (!msgtitle) {
          msgtitle = "系統無回應，請稍後再試"
      }

      Bus.$emit('errors', {
          title: msgtitle,
          status: error.status
      })
   }
   static BusEmitOK(title) {
      let msgtitle = '資料已存檔'
      if (title) msgtitle = title
      let msg = {
          title: msgtitle,
          status: 200
      }

      Bus.$emit('okmsg', msg);
	}
	static tryParseInt(val) {
		if (!val) return 0
		return parseInt(val)
   }
   static isTrue(val) {
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
	static buildQuery(url, searchParams) {
      url += '?'
      for (let field in searchParams) {

          let value = searchParams[field]
          url += field + '=' + value + '&'

      }
      return url.substr(0, url.length - 1);

    }
}

export default Helper;