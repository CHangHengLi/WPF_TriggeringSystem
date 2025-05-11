# WPF触发器系统示例项目

## 项目简介

本项目是一个基于.NET Core 9.0的WPF示例应用，用于演示WPF中各种触发器(Trigger)系统的使用方法和最佳实践。项目采用MVVM(Model-View-ViewModel)架构模式设计，包含多种触发器类型的实际应用场景。

## 运行环境

- .NET Core 9.0 或更高版本
- Windows操作系统
- Visual Studio 2022 或更高版本

## 项目结构

```
TriggerSystemDemo/
├── Commands/          # 命令类实现
├── Converters/        # 值转换器
├── Models/            # 数据模型层
├── Styles/            # 样式和资源定义
├── ViewModels/        # 视图模型层
└── Views/             # 视图层
```

## 主要功能特性

1. **基础触发器示例**
   - 属性触发器(Trigger)
   - 数据触发器(DataTrigger)
   - 多条件触发器(MultiTrigger)
   - 多数据条件触发器(MultiDataTrigger)
   - 事件触发器(EventTrigger)

2. **扩展示例**
   - 触发器优先级演示
   - C#代码创建触发器
   - 触发动作示例(BeginStoryboard, PauseStoryboard等)
   - 自定义登录按钮

3. **表单验证功能**
   - 使用数据触发器实现输入验证
   - 错误提示和反馈

4. **动画效果**
   - 基于触发器的UI动画
   - 过渡效果演示

## 如何使用

1. 克隆或下载本仓库
2. 使用Visual Studio 2022或更高版本打开解决方案文件
3. 构建并运行项目
4. 在程序主界面中选择不同的示例选项卡，体验各种触发器效果

## 示例详解

### 基础触发器

展示了WPF中最常用的几种触发器类型，包括属性触发器、数据触发器和事件触发器。这些示例适合初学者理解触发器的基本概念。

### 多条件触发器

演示如何使用MultiTrigger和MultiDataTrigger来响应多个条件的变化。示例中的高级文本框样式在鼠标悬停且获取焦点时会同时改变背景和边框效果。

### 触发器优先级

通过实际例子展示WPF中属性值来源的优先级顺序：
1. 动画(最高优先级)
2. 本地值(直接在元素上设置)
3. 样式触发器(条件满足时)
4. 模板触发器(条件满足时)
5. 样式Setter(最低优先级)

### C#代码创建触发器

展示如何在代码中动态创建和应用样式及触发器，适用于需要在运行时改变UI行为的场景。

### 触发动作示例

展示了控制动画的不同触发动作，如BeginStoryboard、PauseStoryboard、ResumeStoryboard和StopStoryboard的使用方法。

## MVVM架构说明

本项目严格遵循MVVM架构模式：
- Model: 定义数据结构和业务逻辑
- ViewModel: 作为View和Model之间的中介，处理UI逻辑并提供数据绑定
- View: 负责UI界面展示，通过数据绑定和命令绑定与ViewModel通信

## 学习资源

项目基于《WPF之触发器系统》文档进行实现，建议结合该文档学习使用：
- [WPF之触发器系统.md](WPF之触发器系统.md)

## 许可证

此示例项目仅供学习和教育目的使用。 