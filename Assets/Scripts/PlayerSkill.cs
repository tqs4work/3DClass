using UnityEngine;
using UnityEngine.UI;

public class PlayerSkills : MonoBehaviour
{
    // danh sách các skills của player
    [Header("Player Skills")]
    public skillData[] skills = new skillData[3];

    [Header("Skill UI")]
    public Image[] skillIcons = new Image[3];
    public Image[] skillCooldownOverlays = new Image[3];

    private float[] _cooldownLeft = new float[3];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (var i = 0; i < skills.Length; i++)
        {
            if (skillIcons[i] != null && skills[i] != null)
                skillIcons[i].sprite = skills[i].icon;

            if (skillCooldownOverlays[i] != null)
                skillCooldownOverlays[i].fillAmount = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // tick cooldown của các skill
        // cập nhật UI
        for (var i = 0; i < skills.Length; i++)
        {
            if (skills[i] == null) continue;
            if (_cooldownLeft[i] > 0)
            {
                _cooldownLeft[i] = Mathf.Max(0, _cooldownLeft[i] - Time.deltaTime);
            }

            if (skillCooldownOverlays[i] != null)
            {
                var t = skills[i].cooldown <= 0 ? 0f : _cooldownLeft[i] / skills[i].cooldown;
                skillCooldownOverlays[i].fillAmount = Mathf.Clamp01(t); // t từ 1 -> 0
            }
        }

        // sử dụng skill khi nhấn phím 1,2,3
        if (Input.GetKeyDown(KeyCode.Alpha1)) TryCastSkill(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) TryCastSkill(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) TryCastSkill(2);
    }

    void TryCastSkill(int index)
    {
        if (index < 0 || index >= skills.Length) return;
        var skill = skills[index];
        if (skill == null) return;
        // nếu đang trong thời gian cooldown thì không thể sử dụng skill
        if (_cooldownLeft[index] > 0) return;
        // ghi log (thay bằng thực hiện hiệu ứng skill trong game)
        // phát ra âm thanh
        // phát ra hiệu ứng của skill
        var effect = Instantiate(skill.skillEffectPrefabs,
            transform.position + transform.forward * 1.5f, Quaternion.identity);
        // hủy hiệu ứng sau 2 giây
        Destroy(effect, 2f);
        Debug.Log("Casting skill: " + skill.skillName + " dealing " + skill.damage + " damage.");
        float totalDamage = skill.GetPower();
        // đặt lại thời gian cooldown
        _cooldownLeft[index] = skill.cooldown;
        if (skillCooldownOverlays[index] != null)
            skillCooldownOverlays[index].fillAmount = 1f;

    }
}
