import client from 'webpack-theme-color-replacer/client'
import generate from '@ant-design/colors/lib/generate'
//更改主题
export default {
  getAntdSerials(color) {
    // 淡化（即less的tint）
    const lightens = new Array(9).fill().map((t, i) => {
      return client.varyColor.lighten(color, i / 10)
    })
    // colorPalette变换得到颜色值
    const colorPalettes = generate(color)
    const rgb = client.varyColor.toNum3(color.replace('#', '')).join(',')
    return lightens.concat(colorPalettes).concat(rgb)
  },
  changeColor(newColor) {
    var options = {
      newColors: this.getAntdSerials(newColor), // new colors array, one-to-one corresponde with `matchColors`
      changeUrl(cssUrl) {
        return `/${cssUrl}` // while router is not `hash` mode, it needs absolute path
      }
    }
    return client.changer.changeColor(options, Promise)
  },
  updatePrimaryColor(color) {
    global.tools.loading.start();
    this.changeColor(color).finally(t => {
      setTimeout(function () {
        global.tools.loading.close();
        global.tools.msg('主题加载完成!', '成功');
      }, 500);
    });
  }
}