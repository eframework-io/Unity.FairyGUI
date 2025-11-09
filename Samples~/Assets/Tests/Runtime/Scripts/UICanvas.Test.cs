// Copyright (c) 2025 EFramework Innovation. All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using NUnit.Framework;
using UnityEngine;
using FairyGUI;
using System.Collections.Generic;
using EFramework.Unity.FairyGUI;

/// <summary>
/// TestUICanvas 是 UICanvas 的单元测试。
/// </summary>
public class TestUICanvas
{
    [TestCase(true, TestName = "通过UIPanel")]
    [TestCase(false, TestName = "不通过UIPanel")]
    public void Index(bool byPanel)
    {
        var rootObj = new GameObject("TestCanvas");
        object result;
        UICanvas canvas;
        if (byPanel)
        {
            canvas = rootObj.AddComponent<UICanvas>();

            UIPackage.AddPackage("Package1");
            canvas.packageName = "Package1";
            canvas.componentName = "Component1_2";
            canvas.CreateUI();

            // 测试正常情况
            result = canvas.Index("Child1.Child2", typeof(GComponent));
            Assert.That(result, Is.Not.Null, "应该返回正确的子对象");

            // 测试类型为空
            result = canvas.Index("Child1.Child2", null);
            Assert.That(result, Is.Not.Null, "应该返回正确的子对象");

            // 测试类型错误
            result = canvas.Index("Child1.Child2", typeof(GButton));
            Assert.That(result, Is.Null, "应该返回空的子对象");
        }
        else
        {
            canvas = rootObj.AddComponent<UICanvas>();
            // 创建测试子对象
            var testChild1 = new GameObject("testChild1");
            testChild1.transform.SetParent(rootObj.transform);
            var testChild2 = new GameObject("testChild2");
            testChild2.transform.SetParent(testChild1.transform);
            var boxCollider = testChild2.AddComponent<BoxCollider>();

            // 测试正常情况
            result = canvas.Index("testChild2", typeof(BoxCollider));
            Assert.That(result, Is.EqualTo(boxCollider), "应该返回正确的子对象");
        }

        // 测试路径不存在
        result = canvas.Index("nonExistentPath", typeof(BoxCollider));
        Assert.That(result, Is.Null, "当路径不存在时应该返回null");

        // 测试类型不存在
        result = canvas.Index("testChild2", typeof(MeshRenderer));
        Assert.That(result, Is.Null, "当类型不存在时应该返回null");

        // 清理测试对象
        GameObject.DestroyImmediate(canvas.gameObject);
    }

    [TestCase(true, TestName = "有依赖关系")]
    [TestCase(false, TestName = "没有依赖关系")]
    public void Awake(bool hasDependency)
    {
        // 创建测试对象
        var canvasObj = new GameObject("TestCanvas");
        canvasObj.SetActive(false);
        var canvas = canvasObj.AddComponent<UICanvas>();
        canvas.packagePath = "Package1";
        canvas.packageName = "Component1_2";
        if (hasDependency)
        {
            // 创建测试UIManifest
            var manifest = canvasObj.AddComponent<UIManifest>();
            manifest.PackagePath = "Package1";
            // 创建测试依赖
            var dependencyObj = new GameObject("TestDependency");
            var dependencyManifest = dependencyObj.AddComponent<UIManifest>();
            dependencyManifest.PackagePath = "Package2";
            manifest.Dependency = new List<Object> { dependencyObj };
            dependencyManifest.Dependency = new List<Object>();
            canvas.packageMani = manifest;
        }
        else
        {
            bool loaderCalled = false;
            UICanvas capturedCanvas = null;
            // 设置Loader回调
            UICanvas.Loader = (uiCanvas) =>
            {
                loaderCalled = true;
                capturedCanvas = uiCanvas;
            };

            canvasObj.SetActive(true);

            Assert.That(loaderCalled, Is.True, "Loader回调应该被调用");
            Assert.That(capturedCanvas, Is.EqualTo(canvas), "Loader回调应该接收正确的UICanvas实例");
        }

        if (hasDependency)
        {
            canvasObj.SetActive(true);
            Assert.That(UIPackage.GetByName("Package2"), Is.Not.Null, "Package2应该被加载");
        }

        // 清理测试对象
        GameObject.DestroyImmediate(canvasObj);
    }
}
