using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Moving : MonoBehaviour
{
    [Header ("Stats")]
    [SerializeField] private int _maxHP;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _hpText;
    private int _currentHP;

    [Header ("Movement")] 
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private Transform _transform;
    private SpriteRenderer sprite;
    [SerializeField] private float jumpPower = 10;
    [SerializeField] private float speed = 10;
    [SerializeField] private float groundCheckerRadius = 0.2f;

    public Animator _animator;
    public string _runAnimationKey;
    private bool _facingRight = true;
    private float _startSpeed;  

    private void Start() {
        _startSpeed = speed;
        _currentHP = _maxHP;
        _slider.maxValue = _maxHP;
        _slider.value = _currentHP;
        _hpText.text = _currentHP.ToString();
    }

    public void TakeDamage(int damage)
    {
        _currentHP -= damage;
        _slider.value = _currentHP;
        _hpText.text = _currentHP.ToString();
        Debug.Log(damage);
        if(_currentHP <= 0){
            Die();
        }
    }

  
    private void Die()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {

        //// 1
        /*    if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                _rigidbody2D.velocity = Vector2.right * 10;
            }
            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                _rigidbody2D.velocity = Vector2.right * -10;
            }*/
        //// 2

        ////3
        /*if (Input.GetButton("Horizontal"))
        {
            Run();
        }*/
        /////4
        /* if(Input.GetKey(KeyCode.D))
         {
             transform.Translate(transform.right * speed * Time.deltaTime);
             sprite.flipX = false;
         }
         else if(Input.GetKey(KeyCode.A))
         {
             transform.Translate(-transform.right * speed * Time.deltaTime);
             sprite.flipX = true;
         }*/
       
        float direction = Input.GetAxisRaw("Horizontal");
        Vector2 velocity = _rigidbody2D.velocity;
        var grounded = Physics2D.OverlapCircle(_groundChecker.position, groundCheckerRadius, _whatIsGround);
        
        _animator.SetInteger("Speed", (int)direction);
        _animator.SetBool("Jump", grounded);

        /*var speedX = grounded ? speed * x : velocity.x;*/
        
            _rigidbody2D.velocity = new Vector2(speed * direction, velocity.y);           
        

        if (direction < 0 && _facingRight)
        {
            Flip();
        }
        else if (direction > 0 && !_facingRight)
        {
            Flip();
        }

        if (Input.GetButtonUp("Jump") && grounded)
        {
                _rigidbody2D.velocity = new Vector2(velocity.x, jumpPower);
        }

    

       /* if (Input.GetKey(KeyCode.C))
        {
            _headCollider2D.enabled = false;
        }
        else if(!cellAbove)
        {
            _headCollider2D.enabled = true;
        }*/
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundChecker.position, groundCheckerRadius);
    }

        
    private void Flip()
    {
        _facingRight = !_facingRight;
        _transform.Rotate(0, 180, 0);
    }

    private void  OnCollisionEnter2D(Collision2D other)
    {
        UpSpeed up = other.collider.GetComponent<UpSpeed>();
        if(up != null)
        {
            speed *= up.UpgradePower;
           Invoke(nameof(ResetSpeed), up.UpgradeTime);
           Destroy(up.gameObject);
        }
    }

    private void ResetSpeed()
    {
        speed = _startSpeed;
    }
    
    public void SetHealth(int bonusHealth)
    {
        _currentHP += bonusHealth;

        if(_currentHP > _maxHP)
        {
            _currentHP = _maxHP;
        }
    }
}
