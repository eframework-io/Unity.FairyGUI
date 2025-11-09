# UIUtility

[![NPM](https://img.shields.io/npm/v/io.eframework.unity.fairygui?label=NPM&logo=npm)](https://www.npmjs.com/package/io.eframework.unity.fairygui)
[![UPM](https://img.shields.io/npm/v/io.eframework.unity.fairygui?label=UPM&logo=unity&registry_uri=https://package.openupm.com)](https://openupm.com/packages/io.eframework.unity.fairygui)
[![DeepWiki](https://img.shields.io/badge/DeepWiki-Explore-blue)](https://deepwiki.com/eframework-io/Unity.FairyGUI)
[![Discord](https://img.shields.io/discord/1422114598835851286?label=Discord&logo=discord)](https://discord.gg/XMPx2wXSz3)

提供了一系列简化 UI 组件操作的扩展方法，是一个 FairyGUI 的工具函数集。

## 功能特性

- 快速索引功能：通过名称或路径快速获取 UI 组件的子对象
- 显示状态控制：提供简便的方法控制 UI 组件的显示和隐藏
- 扩展方法支持：采用扩展方法设计，函数调用更为直观

## 使用手册

### 1. 索引操作

#### 1.1 UICanvas 索引

通过 UICanvas 获取子组件：

```csharp
// 获取 UICanvas
var canvas = FindObjectOfType<UICanvas>();

// 通过路径获取按钮组件
var loginBtn = canvas.Index<GButton>("loginPanel.loginBtn");
```

#### 1.2 GComponent 索引

通过 GComponent 获取子组件：

```csharp
// 获取 GComponent
var panel = canvas.ui.GetChild("mainPanel").asCom;

// 通过名称获取按钮
var okBtn = panel.Index<GButton>("okBtn");
```

### 2. 状态控制

#### 2.1 设置组件显示状态

控制 UI 对象的显示状态：

```csharp
// 获取 GObject
var obj = panel.GetChild("notification");

// 显示组件
obj.SetActive(true);

// 隐藏组件
obj.SetActive(false);

// 链式调用
canvas.Index<GButton>("loginBtn")?.SetActive(true);
```

#### 2.2 设置容器显示状态

控制容器及其子对象的显示状态：

```csharp
// 获取容器
var container = panel.GetChild("container").asCom;

// 显示整个容器
container.SetActive(true);

// 隐藏整个容器
container.SetActive(false);
```

#### 2.3 设置子对象显示状态

通过路径控制子对象的显示状态：

```csharp
// 获取容器
var panel = canvas.ui.GetChild("mainPanel").asCom;

// 通过路径显示子对象
panel.SetActive("header.logo", true);

// 通过路径隐藏子对象
panel.SetActive("footer.copyright", false);
```

## 常见问题

### 1. 无法索引子对象？

如果使用 Index 方法无法找到子对象：
- 检查路径名称是否正确（区分大小写）
- 确认子对象确实存在于指定的路径下
- 验证泛型类型是否与实际组件类型匹配

更多问题，请查阅[问题反馈](../CONTRIBUTING.md#问题反馈)。

## 项目信息

- [更新记录](../CHANGELOG.md)
- [贡献指南](../CONTRIBUTING.md)
- [许可协议](../LICENSE.md)
